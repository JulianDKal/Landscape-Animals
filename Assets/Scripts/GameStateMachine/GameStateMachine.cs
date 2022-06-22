using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    GameBaseState currentState;
    
    //all managers and systems references 
    public GameObject GridInstObject;
    public GameObject AnimalManagerObject;
    public HexagonGrid HexGrid {get; set;}
    public AnimalManager AnimalManager {get; set;}

    public Material defaultMaterial;
    public Material highlightMaterial;
    
    void Start()
    {
        // might need another state for setup
        currentState = new GameInputState(this);
        HexGrid = GridInstObject.GetComponent<HexagonGrid>();
        AnimalManager = AnimalManagerObject.GetComponent<AnimalManager>();

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
