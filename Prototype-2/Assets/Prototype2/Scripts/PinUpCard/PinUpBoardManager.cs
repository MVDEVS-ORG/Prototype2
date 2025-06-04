using UnityEngine;

public class PinUpBoardManager : MonoBehaviour
{
    public void EnablePinUpBoard()
    {
        gameObject.SetActive(true);
    }

    public void DisablePinUpBoard()
    {
        gameObject.SetActive(false);
    }
}
