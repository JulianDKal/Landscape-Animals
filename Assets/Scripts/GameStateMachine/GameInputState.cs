using UnityEngine;

public class GameInputState : GameBaseState
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
    private Transform currentSelection;
    
    public GameInputState(GameStateMachine gsm) : base(gsm) {}

    // run at the start of the state
    public override void EnterState()
    {
        AnimalManager.instance.MakeSelectable();
    }

    // run every frame during the state
    public override void UpdateState()
    {
        SelectObjects();
    }

    void SelectObjects()
    {
        string selectableTag = "Selectable";

        if(currentSelection != null)
        {
            var selectionRenderer = currentSelection.GetComponent<Renderer>();
            selectionRenderer.material = gsm.defaultMaterial;
        }


        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();

                if(selectionRenderer != null)
                {
                    selectionRenderer.material = gsm.highlightMaterial;
                    currentSelection = selection;
                }
            }
        }
    }
}