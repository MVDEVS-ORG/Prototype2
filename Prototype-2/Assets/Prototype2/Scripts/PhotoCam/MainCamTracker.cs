using UnityEngine;

public class MainCamTracker : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _mainCamera;

    private void LateUpdate()
    {
        transform.position = _mainCamera.position + _offset;
        transform.rotation = _mainCamera.rotation;
    }
}
