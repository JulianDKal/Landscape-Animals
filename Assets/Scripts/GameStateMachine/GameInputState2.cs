using UnityEngine;

public class GameInputState2 : GameBaseState
{
    /*
        - All animals become selectable
            - Highlight on mouse hover
            - Select on mouse click
        - When animal gets selected, record that animal in gsm
        - All animals become unselectable
        - Show possible tiles to move to (check if legal) based on animal grid location, make them selectable
        - When selected, record in gsm and switch to MoveState

        Note: user might deselect (e.g. by clicking on empty tile), which we could achieve by re-entering InputState.
        - OnMouseDown and OnMouseEnter functionality can be implemented in Animal itself
    */
    private LayerMask actorMask;
    private LayerMask highlightMask;
    private LayerMask selectMask;

    private float maxDistance = 100f;
    private GameObject currentHover;
    private GameObject currentSelection;
    
    public GameInputState2(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        actorMask = LayerMask.NameToLayer("Actor");
        highlightMask = LayerMask.NameToLayer("Highlighted");
        selectMask = LayerMask.NameToLayer("Selected");

        HighlightNeighbors();
    }

    // run every frame during the state
    public override void UpdateState()
    {
        HoverObjects();
        //Deselect() -> go back to input1
    }

    private void HighlightNeighbors()
    {
        Hexagon location = gsm.selectedAnimal.transform.parent.GetComponent<Hexagon>();

        foreach(GameObject neighbour in location.GetNeighbours())
        {
            neighbour.layer = actorMask;
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
                    currentHover.layer = actorMask;
                }

                currentHover = hover;
                currentHover.layer = highlightMask;
            }
        }
        else if(currentHover != null) //if we haven't hit an object
        {
            currentHover.layer = actorMask; //dehighlight
            currentHover = null;
        }
    }

}