using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameBaseState currentState;
    
    public Material defaultMaterial;
    public Material highlightMaterial;
    
    void Start()
    {
        // might need another state for setup
        currentState = new GameInputState(this);

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
