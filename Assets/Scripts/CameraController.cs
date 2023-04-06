using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float sensitivity = 5.0f; 

    [SerializeField] private float distance = 1000f;

    [SerializeField] private float minZoom = 1000f;
    
    [SerializeField] private float maxZoom = 2000f;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * sensitivity;
            y -= Input.GetAxis("Mouse Y") * sensitivity;
        }
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * sensitivity * 5, minZoom, maxZoom);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}