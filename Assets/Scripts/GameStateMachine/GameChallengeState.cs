using UnityEngine;

public class GameChallengeState : GameBaseState
{
    /*
        - Update existing challenges
            - Probably just check if latest tile has any challenges around it and update their counters
            - Other way is to go through each challenge and check their neighbors for any changes (useful if we want to introduce landscape changing on its own)
        - Play number change animation / happy animals
        - Add new challenges according to gsm parameter
        - Move the camera to highlight them properly
        - Move back to previously selected animal
    */

    public GameChallengeState(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {

    }

    // run every frame during the state
    public override void UpdateState()
    {

    }
}