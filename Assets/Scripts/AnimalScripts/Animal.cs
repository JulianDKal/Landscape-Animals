using UnityEngine;

public class Animal : MonoBehaviour
{
    private float hexHeight = 3f;

    public void MoveTo(int q, int r)
    {
        Vector3 animalSize = gameObject.GetComponent<Renderer>().bounds.size;
        GameObject hex = HexagonGrid.instance.GetHexagon(q, r);
        
        gameObject.transform.parent = hex.transform;
        gameObject.transform.position = hex.transform.position + Vector3.up * (animalSize.y + hexHeight) / 2;
    }

    public void RotateTo(int q, int r)
    {
        Hexagon currentHex = gameObject.transform.parent.GetComponent<Hexagon>();
        int direction = currentHex.GetDirection(q, r);

        gameObject.transform.rotation = Quaternion.Euler(0, direction * -60, 0);
    }

    // type of landscape it creates
    // initial location?
}
