using System.Collections;
using System.Collections.Generic;
using Platformer.CoreSystem;
using UnityEngine;

public class MeleeAttackState : AttackState
{
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
    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttacking()
    {
        base.FinishAttacking();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TrigerAttack()
    {
        base.TrigerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer); 

        foreach(Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.Damage(stateData.attackDamage);
                
                int reducePoints = PlayerPrefs.GetInt("ReducePoints", 0);
                reducePoints -= (int)Stats.PointReduce.MaxValue;
                PlayerPrefs.SetInt("ReducePoints", reducePoints);
                PlayerPrefs.Save();
                
                Debug.Log(PlayerPrefs.GetInt("Points", 0));
            }

            IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

            if(knockBackable != null)
            {
                knockBackable.KnockBack(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection);
            }
        }
    }
}
