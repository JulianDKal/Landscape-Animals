using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameBaseState currentState;
    GameBaseState InputState = new GameInputState();
    GameBaseState MoveState = new GameMoveState();
    GameBaseState ChallengeState = new GameChallengeState();
    GameBaseState CleanUpState = new GameCleanUpState();

    [SerializeField]
    public Material defaultMaterial;
    [SerializeField]
    public Material highlightMaterial;
    
    void Start()
    {
        // might need another state for setup
        currentState = InputState;

        currentState.EnterState(this);
    }

    
    void Update()
    {
        // runs every frame
        currentState.UpdateState(this);
    }

    
    public void SwitchState(GameBaseState newState)
    {
        //currentState.ExitState(this);

        currentState = newState;

        newState.EnterState(this);
    }
}
