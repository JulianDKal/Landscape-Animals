using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hexagon : MonoBehaviour
{
    public int q;
    public int r;

    public HexagonGrid.TileTypes tileType = HexagonGrid.TileTypes.Empty;

    readonly Vector2[] directionVectors = new Vector2[]
    {
        new Vector2(+1, 0), new Vector2(+1, -1), new Vector2(0, -1),
        new Vector2(-1, 0), new Vector2(-1, +1), new Vector2(0, +1)
    };

    private void OnMouseDown()
    {
        GetNeighbours();
        CameraController.instance.MoveCameraToObject(gameObject.transform.position);
    }

    public List<GameObject> GetNeighbours()
    {
        Vector2 qr = new Vector2(q, r);

        List<GameObject> neighbours = new List<GameObject>();
        foreach (Vector2 vector in directionVectors)
        {
            Vector2 addedVector = qr + vector;
            if (addedVector.x >= 0 && addedVector.y >= 0 )
            {
                if (HexagonGrid.instance.hexagons[(int)addedVector.x, (int)addedVector.y] != null)
                {
                    neighbours.Add(HexagonGrid.instance.hexagons[(int)addedVector.x, (int)addedVector.y]);
                }
            }
            
        }
        Debug.Log(neighbours.Count);
        return neighbours;
    }
}
