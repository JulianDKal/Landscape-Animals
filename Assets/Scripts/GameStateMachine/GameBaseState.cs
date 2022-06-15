using UnityEngine;

public abstract class GameBaseState
{
    // run at the start of the state
    public abstract void EnterState(GameStateMachine gsm);

    // run every frame during the state
    public abstract void UpdateState(GameStateMachine gsm);

    // run at the end of the state (if needed)
    // public abstract void ExitState(GameStateMachine gsm);
}
