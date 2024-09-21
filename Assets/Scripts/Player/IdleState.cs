using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerMovement player;
    const string IDLE = "Idle";
    public IdleState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.ChangeAnimationState(IDLE);
      
    }

    public void Exit()
    {
     
    }

    public void UpdateState()
    {
        if (player.hInput != 0 && player.isGrounded && !player.IDashing)
        {
            player.StateMachine.TransitionTo(player.StateMachine.runState);
        }
        if (player.Rb.velocity.y > 0 && !player.isGrounded)
        {

            player.StateMachine.TransitionTo(player.StateMachine.jumpState);
        }
        //if(player.hInput != 0 && player.isGrounded && player.IDashing == true)
        //{
        //    player.StateMachine.TransitionTo(player.StateMachine.dashState);
        //}
    }
}
