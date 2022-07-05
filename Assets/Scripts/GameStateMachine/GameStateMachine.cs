using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameBaseState currentState;
    
    public GameObject selectedAnimal;
    public GameObject selectedHex;

    public LayerMask defMask;
    public LayerMask actorMask;
    public LayerMask highlightMask;
    public LayerMask selectMask;
    
    void Start()
    {
        // might need another state for setup
        currentState = new GameInputState1(this);

        defMask = LayerMask.NameToLayer("Default");
        actorMask = LayerMask.NameToLayer("Actor");
        highlightMask = LayerMask.NameToLayer("Highlighted");
        selectMask = LayerMask.NameToLayer("Selected");

        currentState.EnterState();
    }

    
    void Update()
    {
        // runs every frame
        currentState.UpdateState();
    }

    
    public void SwitchState(GameBaseState newState)
    {
        //currentState.ExitState();

        currentState = newState;

        newState.EnterState();
    }
}
