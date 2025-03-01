using Platformer.Weapons.Components;
using UnityEngine;

namespace Platformer.Weapons.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.Movement coreMovement;

        private CoreSystem.Movement CoreMovement =>
            coreMovement ??= Core.GetCoreComponent<CoreSystem.Movement>();
        
        private void HandleStartMovement()
        {
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
             CoreMovement.SetVelocityZero();
        }

        protected override void Start()
        {
            base.Start();
            
            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;  
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}