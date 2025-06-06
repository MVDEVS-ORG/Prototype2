using UnityEngine;

public class PhotoCameraManager : MonoBehaviour
{
    //[SerializeField] MainCamTracker mainCamTracker;
    [SerializeField] GameObject PlayerCamPlane;
    public void EnableSecondCamTracking()
    {
        PlayerCamPlane.SetActive(true);
        gameObject.SetActive(true);
        //mainCamTracker.enabled = true;
    }

    public void DisableSecondCamTracking()
    {
        PlayerCamPlane.SetActive(false);
        gameObject.SetActive(false);
        //mainCamTracker.enabled = false;
    }
}
