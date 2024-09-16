using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    private PlayerMovement player;
    const string FALL = "Fall";
    public FallState(PlayerMovement player)
    {
        this.player = player;
    }

    void IState.Enter()
    {
        player.ChangeAnimationState(FALL);
    }

    void IState.Exit()
    {
        
    }

    void IState.UpdateState()
    {

        if (player.hInput == 0 && player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.idleState);
        }
        if (player.hInput != 0 && player.isGrounded)
        {
            player.StateMachine.TransitionTo(player.StateMachine.runState);
        }
    }
}
