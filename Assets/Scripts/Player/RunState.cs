using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private PlayerMovement player;
    const string RUN = "Run";
    public RunState(PlayerMovement player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.ChangeAnimationState(RUN);
        
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        if (player.hInput == 0 && player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.idleState);
        }
        if (player.Rb.velocity.y > 0 && !player.isGrounded)
        {

            player.StateMachine.TransitionTo(player.StateMachine.jumpState);
        }
        if (player.IDashing == true)
        {
            player.StateMachine.TransitionTo(player.StateMachine.dashState);
        }
    }
}
