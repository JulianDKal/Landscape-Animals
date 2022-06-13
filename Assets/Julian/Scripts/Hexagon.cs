using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("clicked!");
        CameraController.instance.MoveCameraToObject(gameObject.transform.position);
    }
}
