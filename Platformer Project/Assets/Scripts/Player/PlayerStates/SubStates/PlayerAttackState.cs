using System.Collections;
using System.Collections.Generic;
using Platformer.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    public PlayerAttackState(
        Player player, 
        PlayerStateMachine stateMachine, 
        PlayerData playerData, 
        string animBoolName, 
        Weapon weapon
        ): base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;

        this.weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

}
