using UnityEngine;

public class ObjectInAltScene : MonoBehaviour
{
    public void SetVisible()
    {
        gameObject.layer = LayerMask.GetMask("Default");
    }
}
