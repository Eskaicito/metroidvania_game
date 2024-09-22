using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState
{
    private PlayerMovement player;
    const string DASH = "Dash";
    public DashState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.ChangeAnimationState(DASH);
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        if (player.hInput != 0 && player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.runState);
        }
        if (player.hInput == 0 && player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.idleState);
        }
        if (player.Rb.velocity.y < 0 && !player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.fallState);
        }
    }

}
