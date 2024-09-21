using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private PlayerMovement player;
    const string JUMP = "Jump";
    public JumpState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.ChangeAnimationState(JUMP);
        AudioManager.instance.PlaySound(JUMP);
        
    }

    public void Exit()
    { 
    }

    public void UpdateState()
    {
        if (player.Rb.velocity.y < 0 && !player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.fallState);
        }
        if (player.IDashing == true)
        {
            player.StateMachine.TransitionTo(player.StateMachine.dashState);
        }
    }

}
