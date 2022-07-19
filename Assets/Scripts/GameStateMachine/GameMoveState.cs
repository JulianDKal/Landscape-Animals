using UnityEngine;

public class GameMoveState : GameBaseState
{
    /*
        - Take the input animal and tile from gsm
        - Rotate animal to face the destination
        - Change the animal grid coordinates
        - Play the move animation
        - Change the type of current tile where animal stands (and play animation)
        - Will require some showing and hiding of landscape, depending on how we implement it
        - Switch to ChallengeState
    */

    private Animal animal;
    private Hexagon destination;

    public GameMoveState(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        animal = gsm.selectedAnimal.GetComponent<Animal>();
        destination = gsm.selectedHex.GetComponent<Hexagon>();
        
        //animate rotating the animal
        animal.RotateTo(destination.q, destination.r);
        //animate moving the animal to another tile
        animal.MoveTo(destination.q, destination.r);

        animal.gameObject.layer = gsm.defMask;
        destination.gameObject.layer = gsm.defMask;

        gsm.SwitchState(new GameInputState1(gsm));
    }

    // run every frame during the state
    public override void UpdateState()
    {
        

    }
}