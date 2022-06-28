using UnityEngine;

public class GameInputState1 : GameBaseState
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
    
    public GameInputState1(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        actorMask = LayerMask.NameToLayer("Actor");
        highlightMask = LayerMask.NameToLayer("Highlighted");
        selectMask = LayerMask.NameToLayer("Selected");

        AnimalManager.instance.MakeActors();
    }

    // run every frame during the state
    public override void UpdateState()
    {
        HoverObjects();
        SelectObject();
    }

    private void HoverObjects()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance, LayerMask.GetMask("Actor", "Highlighted")))
        {
            var hover = hit.collider.gameObject;
            
            if(currentHover != hover)
            {
                if(currentHover != null)
                {
                    currentHover.layer = actorMask;
                }

                currentHover = hover;
                currentHover.layer = highlightMask;
            }
        }
        else if(currentHover != null)
        {
            currentHover.layer = actorMask;
            currentHover = null;
        }
    }

    private void SelectObject()
    {
        if(currentHover != null && Input.GetMouseButtonDown(0))
        {
            AnimalManager.instance.MakeDefault();

            currentSelection = currentHover;
            currentSelection.layer = selectMask;
            currentHover = null;

            gsm.selectedAnimal = currentSelection;

            gsm.SwitchState(new GameInputState2(gsm));
        }
    }
}