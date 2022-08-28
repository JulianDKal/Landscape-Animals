using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class RequirementsClass : MonoBehaviour
{
    /// <summary>
    /// This is just a helper class for now since I wasn't sure in which class to put this. 
    /// </summary>
    [SerializeField]
    private List<HexagonGrid.TileTypes> _requestedTiles = new List<HexagonGrid.TileTypes>();

    [SerializeField] 
    private int numOfRequirements = 4;

    [SerializeField]
    private GameObject challengeButtonPrefab;

    public GameObject partnerChallengeButton;

    private void Start()
    {
        MakeChallenge();
    }

    public int TilesFulfilled
    {
        get { return CalculateNumberOfTilesFulfilled();}
        set { return; }
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Fulfilled:" + CalculateNumberOfTilesFulfilled() + "/" + _requestedTiles.Count);
        ChallengeButton();
    }

    private void OnMouseExit()
    {
        ChallengeButton();
    }

    int CalculateNumberOfTilesFulfilled()
    {
        int count = 0;
        List<GameObject> surroundingObjects = gameObject.GetComponent<Hexagon>().GetNeighbours();
        List<HexagonGrid.TileTypes> requestedTilesFloating = new List<HexagonGrid.TileTypes>();
        //adding the items of requestedTiles to this list so requestedTiles doesn't get touched and stays the same
        foreach (var item in _requestedTiles)
        {
            requestedTilesFloating.Add(item);
        }
        for (int i = 0; i < surroundingObjects.Count; i++)
        {
            HexagonGrid.TileTypes currentTileType = surroundingObjects[i].GetComponent<Hexagon>().tileType;
            //check if the tile type of the current tile is requested by the challenge. If yes, remove the first entry of it from the 
            //requestedTilesFloating list so it doesn't trigger the if-statement in the next iteration of the loop.  
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
        return CalculateNumberOfTilesFulfilled() >= _requestedTiles.Count;
    }

    private void ChallengeButton()
    {
        if (partnerChallengeButton == null)
        {
            GameObject button = Instantiate(challengeButtonPrefab, GameObject.Find("Canvas").transform, false);
            //setting the partner hexagon of the button to this object so the button always stays on this hexagon
            button.GetComponent<ChallengeButtonLogic>().partnerHexagon = gameObject.transform;
            button.transform.localScale = new Vector3(0, 0, 0);
            button.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            button.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            int numOfFulfilled = CalculateNumberOfTilesFulfilled();
            button.GetComponentInChildren<TMP_Text>().text = numOfFulfilled + "/" + _requestedTiles.Count;
            partnerChallengeButton = button;
            button.GetComponent<ChallengeButtonLogic>().SetTransform();
        }
        //set button active if it isn't, deactivate it if it is active
        else
        {
            partnerChallengeButton.SetActive(!partnerChallengeButton.activeSelf);
            partnerChallengeButton.GetComponent<ChallengeButtonLogic>().SetTransform();
        }
        
    }

    private void MakeChallenge()
    {
        List<HexagonGrid.TileTypes> requestedTiles = new List<HexagonGrid.TileTypes>();
        for (int i = 0; i < numOfRequirements; i++)
        {
            int num = Random.Range(2, 6);
            requestedTiles.Add((HexagonGrid.TileTypes)num);
            _requestedTiles = requestedTiles;
        }
    }
}
