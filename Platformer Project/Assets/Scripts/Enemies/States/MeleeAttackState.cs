using System.Collections;
using System.Collections.Generic;
using Platformer.CoreSystem;
using Platformer.Combat.Damage;
using Platformer.Combat.KnockBack;
using Platformer.Combat.PoiseDamage;
using UnityEngine;

public class MeleeAttackState : AttackState {
    protected Movement Movement
    {
        get => movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement movement;
    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
    }
    private CollisionSenses collisionSenses;
    
    private Stats Stats  => stats ??= core.GetCoreComponent<Stats>(); 
    private Stats stats;

    protected D_MeleeAttack stateData;

    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(etity, stateMachine, animBoolName, attackPosition) {
        this.stateData = stateData;
    }
	
    public override void TriggerAttack() {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects) {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null) {
                damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
                
                int reducePoints = PlayerPrefs.GetInt("ReducePoints", 0);
                reducePoints -= (int)Stats.PointReduce.MaxValue;
                PlayerPrefs.SetInt("ReducePoints", reducePoints);
                PlayerPrefs.Save();
                
                Debug.Log(PlayerPrefs.GetInt("Points", 0));
            }

            IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

            if (knockBackable != null) {
                knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
            }

            if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(new PoiseDamageData(stateData.PoiseDamage, core.Root));
            }
        }
    }
}