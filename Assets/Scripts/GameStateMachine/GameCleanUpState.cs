using UnityEngine;

public class GameCleanUpState : GameBaseState
{

    /*
        - Remove animals that used their moves (or until some other condition is met)
        - Spawn new animals if needed
        - Make other necessary changes to UI like challenge update / turn counter
        - Switch to InputState
    */

    // run at the start of the state
    public override void EnterState(GameStateMachine gsm)
    {

    }

    // run every frame during the state
    public override void UpdateState(GameStateMachine gsm)
    {

    }
}