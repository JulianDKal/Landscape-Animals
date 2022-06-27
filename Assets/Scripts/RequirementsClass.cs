using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequirementsClass : MonoBehaviour
{
    /// <summary>
    /// This is just a helper class for now since I wasn't sure in which class to put this. 
    /// </summary>
    [SerializeField]
    private List<HexagonGrid.TileTypes> requestedTiles = new List<HexagonGrid.TileTypes>();

    public int TilesFulfilled
    {
        get { return CalculateNumberOfTilesFulfilled();}
        set { return; }
    }

    private void OnMouseDown()
    {
        Debug.Log("Fulfilled:" + CalculateNumberOfTilesFulfilled() + "/" + requestedTiles.Count);
        Debug.Log(ChallengeIsDone());
    }

    int CalculateNumberOfTilesFulfilled()
    {
        int count = 0;
        List<GameObject> surroundingObjects = gameObject.GetComponent<Hexagon>().GetNeighbours();
        List<HexagonGrid.TileTypes> requestedTilesFloating = new List<HexagonGrid.TileTypes>();
        //adding the items of requestedTiles to this list so requestedTiles doesn't get touched and stays the same
        foreach (var item in requestedTiles)
        {
            requestedTilesFloating.Add(item);
        }
        for (int i = 0; i < surroundingObjects.Count; i++)
        {
            HexagonGrid.TileTypes currentTileType = surroundingObjects[i].GetComponent<Hexagon>().tileType;
            if (requestedTilesFloating.Contains(currentTileType))
            {
                count++;
                requestedTilesFloating.Remove(requestedTilesFloating.First(a => a == currentTileType));
                
            }
        }
        //Debug.Log(requestedTilesFloating.Count);
        return count;
    }

    public bool ChallengeIsDone()
    {
        return CalculateNumberOfTilesFulfilled() >= requestedTiles.Count;
    }
}
