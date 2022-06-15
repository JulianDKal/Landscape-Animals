using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGrid : MonoBehaviour
{
    public static HexagonGrid instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    Vector3 startPos = Vector3.zero;
    public GameObject[,] hexagons;
    
    [SerializeField]
    private float hexWidth = 1f;
    [SerializeField]
    private float hexHeight = 1f;
    [SerializeField]
    private float gap = 0;
    [SerializeField]
    private GameObject HexagonObj;
    [SerializeField]
    private int gridLength = 3;
    [SerializeField]
    private int gridHeight = 3;

    void Start()
    {
        hexHeight += gap;
        hexWidth += gap;
        //CreateRectangularGrid();
        CreateHexShapedGrid();
    }

    void CreateRectangularGrid()
    {
        hexagons = new GameObject[gridHeight, gridLength];

        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridLength; y++)
            {
                GameObject newHexagon = Instantiate(HexagonObj, Vector3.zero, Quaternion.identity);
                //-x to reverse the direction in which the grid gets instantiated to make it odd-q
                newHexagon.transform.position = new Vector3(-x * hexWidth + (hexWidth/4) * x, 0, y * hexHeight);
                hexagons[x,y] = newHexagon;
                if(x % 2 != 0)
                {
                    newHexagon.transform.position = new Vector3(-x * hexWidth + (hexWidth/4) * x, 0, y * hexHeight + hexHeight/2);
                }
                newHexagon.name = "Hexagon" + x + "|" + y;
                newHexagon.GetComponent<Hexagon>().x = x;
                newHexagon.GetComponent<Hexagon>().y = y;
            }   
        }
    }

    [SerializeField]
    private int HexagonGridSize = 2;

    void CreateHexShapedGrid()
    {
        List<Hex> hexes = new List<Hex>();
        for (int q = -HexagonGridSize; q <= HexagonGridSize; q++)
        {
            for (int r = -HexagonGridSize; r <= HexagonGridSize; r++)
            {
                for (int s = -HexagonGridSize; s <= HexagonGridSize; s++)
                {
                    if (q + r + s == 0)
                    {
                        Hex newHex = new Hex(q, r, s);
                        Debug.Log(q + "," + r + "," + s);
                        hexes.Add(newHex);
                    }
                }
                
            }
        }

        foreach (Hex hex in hexes)
        {
            Vector3 position = HexPosition(hex);
            GameObject newHexagon = Instantiate(HexagonObj, position, Quaternion.identity);
            newHexagon.GetComponent<Hexagon>().x = hex.q;
            newHexagon.GetComponent<Hexagon>().y = hex.r;
        }
    }

    Vector3 HexPosition(Hex hex)
    {
        Vector3 position = Vector3.zero;
        position.z = hexWidth/2 * (Mathf.Sqrt(3) * hex.q + Mathf.Sqrt(3)/2 * hex.r);
        position.x = hexWidth * 3/4  * (                   3 / 2 * hex.r);

        return position;
    }
}
