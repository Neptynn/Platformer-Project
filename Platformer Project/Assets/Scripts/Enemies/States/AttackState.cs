using System.Collections;
using System.Collections.Generic;
using Platformer.CoreSystem;
using UnityEngine;

public class AttackState : State {

    private Movement movement;

    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName) {
        this.attackPosition = attackPosition;

        movement = core.GetCoreComponent<Movement>();
    }

    public override void DoChecks() {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter() {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinished = false;
        movement?.SetVelocityX(0f);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        movement?.SetVelocityX(0f);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack() {

    }

    public virtual void FinishAttack() {
        isAnimationFinished = true;
    }
}
