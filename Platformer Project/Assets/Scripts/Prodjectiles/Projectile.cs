using UnityEngine;
using Platformer.Combat.Damage;
using Platformer.Combat.KnockBack;
using Platformer.Combat.PoiseDamage;
using Platformer.CoreSystem;

namespace Platformer.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        
        private float damage;
        private float speed;
        private float travelDistance;
        private float xStartPos;
        private int reducePoints;
        private Vector2 knockbackAngle;
        private float knockbackStrength;
        private float poiseDamage;
        private int FacingDirection;
        
        [SerializeField] private float gravity;
        [SerializeField] private float damageRadius;

        private Rigidbody2D rb;

        private bool isGravityOn;
        private bool hasHitGround;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform damagePosition;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            rb.gravityScale = 0.0f;
            rb.velocity = transform.right * speed;

            isGravityOn = false;

            xStartPos = transform.position.x;
        }

        private void Update()
        {
            if (!hasHitGround)
            {
                //attackDetails.position = transform.position;

                if (isGravityOn)
                {
                    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!hasHitGround)
            {
                Collider2D[] damageHit = Physics2D.OverlapCircleAll(damagePosition.position, damageRadius, whatIsPlayer);
                Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                foreach (Collider2D collider in damageHit)
                {
                    IDamageable damageable = collider.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.Damage(new DamageData(damage, collider.gameObject));
                        Destroy(gameObject);
                        
                        int newPoints = PlayerPrefs.GetInt("ReducePoints", 0);
                        newPoints -= reducePoints;
                        PlayerPrefs.SetInt("ReducePoints", newPoints);
                        PlayerPrefs.Save();
                        
                    }
                    
                    IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

                    if (knockBackable != null) {
                        knockBackable.KnockBack(new KnockBackData(knockbackAngle, knockbackStrength, FacingDirection, collider.gameObject));
                    }

                    if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
                    {
                        poiseDamageable.DamagePoise(new PoiseDamageData(poiseDamage, collider.gameObject));
                    }
                    
                }
                if (groundHit)
                {
                    hasHitGround = true;
                    rb.gravityScale = 0f;
                    rb.velocity = Vector2.zero;
                }


                if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
                {
                    isGravityOn = true;
                    rb.gravityScale = gravity;
                }
               
            }
        }

        public void FireProjectile(float speed, float travelDistance, float damage, Vector2 knockbackAngle, float knockbackStrength, float poiseDamage, int reducePoints, int FacingDirection)
            {
                this.speed = speed;
                this.travelDistance = travelDistance;
                this.damage = damage;
                this.reducePoints = reducePoints;
                this.knockbackAngle = knockbackAngle;
                this.knockbackStrength = knockbackStrength;
                this.poiseDamage = poiseDamage;
                this.FacingDirection = FacingDirection;
            }

            private void OnDrawGizmos()
            {
                Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
            }
        }
    }

