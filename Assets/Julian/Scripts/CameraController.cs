using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField, Tooltip("The lower the value, the more the camera 'slides' after releasing the button")]
    private float slidingEffect = 1f;

    Transform cam;

    #region RotatingCamera
    Vector2 mouseDownPosition = Vector2.zero;
    Vector2 currentMousePos = Vector2.zero;
    float distanceVector = 0;
    [SerializeField]
    float rotationSpeed = 3f;
    [SerializeField, Tooltip("See sliding effect tooltip")]
    float cameraSlidingEffect = 1f;
    Quaternion newRotation;
    #endregion

    #region ZoomingCamera
    Vector3 zoomPos;
    [SerializeField]
    Vector3 zoomAmount;
    [SerializeField]
    float zoomSlidingEffect = 3f;

    public float minZoom;
    public float maxZoom;
    #endregion

    Vector3 newPosition;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        cam = Camera.main.transform;
        zoomPos = cam.localPosition;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        newPosition += horizontal * movementSpeed * Time.deltaTime * new Vector3(cam.right.x, 0, cam.right.z);
        newPosition += vertical * movementSpeed * Time.deltaTime * new Vector3(cam.forward.x, 0, cam.forward.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * slidingEffect);
        //Debug.Log(cam.forward + "," + cam.right);

        if (Input.GetMouseButtonDown(1))
        {
            mouseDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            currentMousePos = Input.mousePosition;
            distanceVector = (currentMousePos.x - mouseDownPosition.x);
            newRotation = Quaternion.Euler(transform.rotation.eulerAngles + (transform.up * distanceVector * Time.deltaTime * rotationSpeed));
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * cameraSlidingEffect);

        if(Input.mouseScrollDelta.y != 0)
        {
            zoomPos += zoomAmount * Input.mouseScrollDelta.y;
            zoomPos.y = Mathf.Clamp(zoomPos.y, minZoom, maxZoom);
            zoomPos.z = Mathf.Clamp(zoomPos.z, -maxZoom, -minZoom);
        }
        cam.localPosition = Vector3.Lerp(cam.localPosition, zoomPos, Time.deltaTime * zoomSlidingEffect);
    }

    void MoveCameraToObject(GameObject targetObject)
    {
        Vector3 targetObjPosition = targetObject.transform.position;
        
    }
}
