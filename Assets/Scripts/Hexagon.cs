using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hexagon : MonoBehaviour
{
    public int x;
    public int y;

    private void OnMouseDown()
    {
        Debug.Log(x + "," + y);
        CameraController.instance.MoveCameraToObject(gameObject.transform.position);
    }

    public List<GameObject> GetNeighbours()
    {
        List<GameObject> neighbours = new List<GameObject>();
        //gets the gameobject at the given ID
        if(HexagonGrid.instance.hexagons.GetValue(x,y) != null)
        {

        }
        return neighbours;
    }
}
