using UnityEngine;

public class ObjectCameraVisible : MonoBehaviour
{
    private bool IsVisible(Camera photographCamera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(photographCamera);
        foreach (Plane plane in planes)
        {
            if(plane.GetDistanceToPoint(gameObject.transform.position) <0 )
            {
                return false;
            }
        }
        return true;
    }

    private bool IsInLOS(Camera photographCamera, LayerMask layerMask)
    {
        if(Physics.Raycast(photographCamera.transform.position,(transform.position - photographCamera.transform.position),out RaycastHit hitInfo,20f,layerMask))
        {
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
