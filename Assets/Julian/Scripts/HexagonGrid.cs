using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGrid : MonoBehaviour
{
    Vector3 startPos = Vector3.zero;
    GameObject[,] hexagons;
    
    [SerializeField]
    private float hexWidth = 1f;
    [SerializeField]
    private float hexHeight = 1f;
    [SerializeField]
    private float gap = 0;
    [SerializeField]
    private GameObject Hexagon;
    [SerializeField]
    private int gridLength = 3;
    [SerializeField]
    private int gridHeight = 3;

    
    void Start()
    {
        hexHeight += gap;
        hexWidth += gap;
        CreateGrid();
    }

    void CreateGrid()
    {
        hexagons = new GameObject[gridHeight, gridLength];

        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridLength; y++)
            {
                GameObject newHexagon = Instantiate(Hexagon, Vector3.zero, Quaternion.identity);
                newHexagon.transform.position = new Vector3(x * hexHeight - (hexHeight/4) * x, 0, y * hexWidth);
                hexagons[x,y] = newHexagon;
                if(x % 2 != 0)
                {
                    newHexagon.transform.position = new Vector3(x * hexHeight - (hexHeight/4) * x, 0, y * hexWidth + hexWidth/2);
                }
                newHexagon.name = "Hexagon" + x + "|" + y;
            }   
        }
    }
}
