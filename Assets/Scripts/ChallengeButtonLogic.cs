using UnityEngine;
using UnityEngine.UI;

public class ChallengeButtonLogic : MonoBehaviour
{
    //The hexagon the challenge button sits on
    public Transform partnerHexagon;
    public float yOffset = 20f;
    private RectTransform _transform;
    void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        _transform.position = Camera.main.WorldToScreenPoint(partnerHexagon.position) + (Vector3.up * yOffset);
    }
}
