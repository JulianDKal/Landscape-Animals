using UnityEngine;

public class GameMoveState : GameBaseState
{
    /*
        - Take the input animal and tile from gsm
        - Change the animal grid coordinates
        - Play the move animation
        - Change the type of current tile where animal stands (and play animation)
        - Will require some showing and hiding of landscape, depending on how we implement it
        - Switch to ChallengeState
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