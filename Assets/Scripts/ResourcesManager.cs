using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI turnLabel;
    [SerializeField]
    private TextMeshProUGUI pointsLabel;

    void Start()
    {
        GameStateMachine.turnEnded += NextTurn;
        GameStateMachine.questFulfilled += AddPoints;
    }

    public void NextTurn()
    {
        Game_Manager.instance.turnCount++;
        turnLabel.text = "Turns: " + Game_Manager.instance.turnCount.ToString();
        
    }

    private void AddPoints()
    {
        pointsLabel.text = Game_Manager.instance.pointCount.ToString();
    }
}
