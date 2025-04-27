using UnityEngine;
using System;

namespace Platformer.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        public event Action  OnMinHoldPassed;
        
        public event Action<AttackPhases> OnEnterAttackPhase;
        
        
        private void AnimationFinidhedTrigger() => OnFinished?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
        
        private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
        

    }
}