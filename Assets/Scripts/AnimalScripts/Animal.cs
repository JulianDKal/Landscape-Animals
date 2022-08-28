using UnityEngine;

public class Animal : MonoBehaviour
{
    // type of landscape it creates
    public GameObject relatedHexagon;
    public HexagonGrid.TileTypes unaccessableHexagons = HexagonGrid.TileTypes.None;

    private float hexHeight = 3f;
    public bool alreadyMoved = false;

    public void MoveTo(int q, int r)
    {
        Vector3 animalSize = gameObject.GetComponent<Renderer>().bounds.size;
        GameObject hex = HexagonGrid.instance.GetHexagon(q, r);
        
        gameObject.transform.parent = hex.transform;
        gameObject.transform.position = hex.transform.position + Vector3.up * (animalSize.y + hexHeight) / 2;

        //instantiate the corresponding hexagon for the animal (e.g. grasslands for sheep)
        //in the position of the old hexagon
        Vector3 hexPosition = hex.transform.position;
        this.gameObject.transform.parent = null;
        Destroy(hex);
        GameObject newHexagon = Instantiate(relatedHexagon, hexPosition, Quaternion.Euler(0, Random.Range(0, 6) * 60, 0));
        this.gameObject.transform.parent = newHexagon.transform;
        newHexagon.GetComponent<Hexagon>().q = q;
        newHexagon.GetComponent<Hexagon>().r = r;
        //add new Hexagon to the list in HexagonGrid class
        HexagonGrid.instance.hexagons[q, r] = newHexagon;
        alreadyMoved = true;
    }

    public void RotateTo(int q, int r)
    {
        Hexagon currentHex = gameObject.transform.parent.GetComponent<Hexagon>();
        int direction = currentHex.GetDirection(q, r);

        gameObject.transform.rotation = Quaternion.Euler(0, direction * -60, 0);
    }

    // initial location?
}
