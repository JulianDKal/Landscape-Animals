using UnityEngine;

public abstract class GameBaseState
{
    protected GameStateMachine gsm;

    public GameBaseState(GameStateMachine gameStateMachine)
    {
        gsm = gameStateMachine;
    }

    // run at the start of the state
    public abstract void EnterState();

    // run every frame during the state
    public abstract void UpdateState();

    // run at the end of the state (if needed)
    // public abstract void ExitState();
}
