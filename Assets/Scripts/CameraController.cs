using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float sensitivity = 5.0f; 

    [SerializeField] private float distance = 1000f;

    [SerializeField] private float minZoom = 1000f;
    
    [SerializeField] private float maxZoom = 2000f;

    private float _x;
    private float _y;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _x = angles.y;
        _y = angles.x;
    }

    void LateUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            _x += Input.GetAxis("Mouse X") * sensitivity;
            _y -= Input.GetAxis("Mouse Y") * sensitivity;
        }
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * sensitivity * 5, minZoom, maxZoom);

        Quaternion rotation = Quaternion.Euler(_y, _x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        
        Transform cachedTransform = transform;
        cachedTransform.rotation = rotation;
        cachedTransform.position = position;
        
    }
}