using UnityEngine;

public class ObjectCameraVisible : MonoBehaviour
{
    private bool IsVisible(Camera photographCamera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(photographCamera);
        foreach (Plane plane in planes)
        {
            if(plane.GetDistanceToPoint(transform.GetComponent<Collider>().bounds.center) <0 )
            {
                return false;
            }
        }
        return true;
    }

    private bool IsInLOS(Camera photographCamera, LayerMask layerMask)
    {
        if(Physics.Raycast(photographCamera.transform.position,(transform.GetComponent<Collider>().bounds.center - photographCamera.transform.position),out RaycastHit hitInfo,20f,layerMask))
        {
            Debug.DrawRay(photographCamera.transform.position, (transform.GetComponent<Collider>().bounds.center - photographCamera.transform.position) * 5, Color.red);
            if (hitInfo.transform == gameObject.transform)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsWithinCamFrustrum(Camera photographCamera, LayerMask layerMask)
    {
        return (IsVisible(photographCamera) && IsInLOS(photographCamera, layerMask));
    }
}
