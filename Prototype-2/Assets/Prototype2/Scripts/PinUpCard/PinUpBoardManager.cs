using UnityEngine;

public class PinUpBoardManager : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void EnablePinUpBoard()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameObject.SetActive(true);
    }

    public void DisablePinUpBoard()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        gameObject.SetActive(false);
    }
}
