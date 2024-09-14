using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;

    public IState CurrentState => currentState;

    public IdleState idleState;
    public FallState fallState;
    public RunState runState;
    public JumpState jumpState;
    public DashState dashState;

    public StateMachine(PlayerMovement player)
    {
        this.fallState = new FallState(player);
        this.runState = new RunState(player);
        this.idleState = new IdleState(player);
        this.jumpState = new JumpState(player);
        this.dashState = new DashState(player);


    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void TransitionTo(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void UpdateState()
    {
        currentState.UpdateState();
    }

}
