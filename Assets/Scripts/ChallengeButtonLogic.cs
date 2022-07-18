using DG.Tweening;
using UnityEngine;
using System.Collections;

public class ChallengeButtonLogic : MonoBehaviour
{
    //The hexagon the challenge button sits on
    public Transform partnerHexagon;
    public float yOffset = 20f;
    private RectTransform _transform;
    private Camera _camera;
    private Vector3 _targetScale;
    void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _camera = Camera.main;
    }
    
    void Update()
    {
        float distanceFromCamera =
            Mathf.Abs((partnerHexagon.transform.position - _camera.transform.position).magnitude);
        _transform.position = _camera.WorldToScreenPoint(partnerHexagon.position) +
                              //fixing the issue that buttons further away from the camera spawn too high
                              (Vector3.up * (yOffset * Mathf.InverseLerp(200, 40, distanceFromCamera)));
        //Debug.Log(Mathf.InverseLerp(140, 40, distanceFromCamera));
        
    }

    public void SetTransform()
    {
        float distanceFromCamera =
            Mathf.Abs((partnerHexagon.transform.position - _camera.transform.position).magnitude);
        _targetScale = Vector3.one * Mathf.InverseLerp(240, 40, distanceFromCamera);
        transform.DOScale(_targetScale, 0.3f);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
}
