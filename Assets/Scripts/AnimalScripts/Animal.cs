using UnityEngine;

public class Animal : MonoBehaviour
{
    // current location on a grid
    public GameObject hex;
    private float hexHeight = 3f;

    public void MoveTo(int q, int r)
    {
        Vector3 animalSize = gameObject.GetComponent<Renderer>().bounds.size;
        hex = HexagonGrid.instance.GetHexagon(q, r);
        
        gameObject.transform.parent = hex.transform;
        gameObject.transform.position = hex.transform.position + Vector3.up * (animalSize.y + hexHeight) / 2;
    }

    // type of landscape it creates
    // initial location?
}
