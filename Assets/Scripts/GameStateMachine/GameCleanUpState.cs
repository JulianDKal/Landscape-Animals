using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCleanUpState : GameBaseState
{
    /*
        - Remove animals that used their moves (or until some other condition is met)
        - Spawn new animals if needed
        - Make other necessary changes to UI like challenge update / turn counter
        - Switch to InputState
    */

    public GameCleanUpState(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        //resetting the animal movements
        foreach (GameObject animal in AnimalManager.instance.animals)
        {
            animal.GetComponent<Animal>().alreadyMoved = false;
        }
        gsm.SwitchState(new GameInputState1(gsm));
    }

    // run every frame during the state
    public override void UpdateState()
    {

    }
}