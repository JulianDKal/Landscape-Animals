using UnityEngine;

public class GameInputState2 : GameBaseState
{
    /*
        - Starts of with animal selected
        - Gives player an option to pick a destination tile
    */

    private float maxDistance = 100f;
    private GameObject currentHover;
    private GameObject currentSelection;
    
    public GameInputState2(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        HighlightNeighbors();
    }

    // run every frame during the state
    public override void UpdateState()
    {
        HoverObjects();
        SelectObject();
        //Deselect() -> go back to input1
    }

    private void HighlightNeighbors()
    {
        Hexagon location = gsm.selectedAnimal.transform.parent.GetComponent<Hexagon>();

        foreach(GameObject neighbour in location.GetNeighbours())
        {
            neighbour.layer = gsm.actorMask;
        } 
    }

    private void HoverObjects()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        //if we hit an object collider
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance, LayerMask.GetMask("Actor", "Highlighted")))
        {
            var hover = hit.collider.gameObject;
            
            //and it is not the same object we are currently on
            if(currentHover != hover)
            {
                if(currentHover != null) //dehighlight if it's not null
                {
                    currentHover.layer = gsm.actorMask;
                }

                currentHover = hover;
                currentHover.layer = gsm.highlightMask;
            }
        }
        else if(currentHover != null) //if we haven't hit an object
        {
            currentHover.layer = gsm.actorMask; //dehighlight
            currentHover = null;
        }
    }

    private void SelectObject()
    {
        if(currentHover != null && Input.GetMouseButtonDown(0))
        {
            HexagonGrid.instance.MakeDefault();
            AnimalManager.instance.MakeDefault();

            gsm.selectedHex = currentHover;

            gsm.SwitchState(new GameMoveState(gsm));
        }
    }
}