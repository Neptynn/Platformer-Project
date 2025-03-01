using Platformer.CoreSystem;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace Platformer.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;
        
        private CoreComp<CoreSystem.Movement> movement;
        private Vector2 offset;
        
        private Collider2D[] detected;
        
        private void HendleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.center.y
                );

            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
                return;
            
            OnDetectedCollider2D?.Invoke(detected);
        }

        protected override void Start()
        {
            base.Start();

            movement = new CoreComp<CoreSystem.Movement>(Core);
            
            eventHandler.OnAttackAction += HendleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnAttackAction -= HendleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if(data == null)
                return;

            foreach (var item in data.AttackData)
            {
                if(!item.Debug)
                    continue;;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
    }
}