using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("CreateHexShapedGrid");
        }
    }

    public GameObject[,] hexagons;
    
    [SerializeField]
    private float hexWidth = 1f;
    [SerializeField]
    private float hexHeight = 1f;
    [SerializeField]
    private float gap = 0;
    [SerializeField]
    private int gridLength = 3;
    [SerializeField]
    private int gridHeight = 3;
    [SerializeField, Tooltip("If true, the Hexagons get instantiated with a random rotation. Otherwise, they always have the same rotation.")]
    private bool haveRandomRotation = true;

    public List<Vector2Int> spawnTilePositions = new List<Vector2Int>();
    public GameObject spawnTile;
    public List<GameObject> Tiles = new List<GameObject>();

    void Start()
    {
        hexHeight += gap;
        hexWidth += gap;
        //StartCoroutine("CreateHexShapedGrid");
        //MakeSpawnTiles();
    }

    void CreateRectangularGrid()
    {
        hexagons = new GameObject[gridHeight, gridLength];

        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridLength; y++)
            {
                GameObject newHexagon = Instantiate(Tiles[Random.Range(0, Tiles.Count)], Vector3.zero, Quaternion.identity);
                //-x to reverse the direction in which the grid gets instantiated to make it odd-q
                newHexagon.transform.position = new Vector3(-x * hexWidth + (hexWidth/4) * x, 0, y * hexHeight);
                hexagons[x,y] = newHexagon;
                if(x % 2 != 0)
                {
                    newHexagon.transform.position = new Vector3(-x * hexWidth + (hexWidth/4) * x, 0, y * hexHeight + hexHeight/2);
                }
                newHexagon.name = "Hexagon" + x + "|" + y;
                newHexagon.GetComponent<Hexagon>().q = x;
                newHexagon.GetComponent<Hexagon>().r = y;
            }   
        }
    }

    [SerializeField]
    private int HexagonGridSize = 2;

    IEnumerator CreateHexShapedGrid()
    {
        hexagons = new GameObject[HexagonGridSize * 2 + 2, HexagonGridSize * 2 + 2];
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
                        //Debug.Log(q + "," + r + "," + s);
                        hexes.Add(newHex);
                    }
                }
                
            }
        }

        foreach (Hex hex in hexes)
        {
            Vector3 position = HexPosition(hex);
            position.y -= 10;
            GameObject newHexagon = Instantiate(Tiles[Random.Range(0, Tiles.Count)], position, HexagonRotation());
            //The hex class is used for instantiating the grid. In the Hexagon class, the IDs of the tiles are stored with the gridsize added because an array couldn't handle 
            //the negative values.
            newHexagon.GetComponent<Hexagon>().q = hex.q + HexagonGridSize;
            newHexagon.GetComponent<Hexagon>().r = hex.r + HexagonGridSize;
            hexagons[hex.q + HexagonGridSize, hex.r + HexagonGridSize] = newHexagon;
            newHexagon.transform.DOMoveY(0, 0.14f, false);
            yield return new WaitForSeconds(0.04f);
        }
    }

    Vector3 HexPosition(Hex hex)
    {
        Vector3 position = Vector3.zero;
        position.z = (hexWidth/2 + gap) * (Mathf.Sqrt(3) * hex.q + Mathf.Sqrt(3)/2 * hex.r);
        position.x = (hexWidth * 3/4 + gap)  * (                   3 / 2 * hex.r);

        return position;
    }

    Quaternion HexagonRotation()
    {
        if (!haveRandomRotation) return Quaternion.identity;
        else return Quaternion.Euler(0, Random.Range(0, 6) * 60, 0);
    }

    void MakeSpawnTiles()
    {
        foreach (Vector2Int spawnTilePos in spawnTilePositions)
        {
            Vector2Int inGridPosition = new Vector2Int(spawnTilePos.x + HexagonGridSize, spawnTilePos.y + HexagonGridSize);
            Vector3 position = hexagons[inGridPosition.x, inGridPosition.y].transform.position;
            Destroy(hexagons[inGridPosition.x, inGridPosition.y]);
            GameObject newSpawnTile = Instantiate(spawnTile, position, Quaternion.identity);
            hexagons[inGridPosition.x, inGridPosition.y] = newSpawnTile;
            newSpawnTile.GetComponent<Hexagon>().tileType = TileTypes.SpawnTile;
        }
    }
    
    public enum TileTypes
    {
        Forest,
        Grasslands,
        Water,
        Mountains,
        SpawnTile,
        Empty
    }
}
