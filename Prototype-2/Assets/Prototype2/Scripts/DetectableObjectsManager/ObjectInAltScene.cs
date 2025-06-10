using UnityEngine;

public class ObjectInAltScene : MonoBehaviour
{
    public void SetVisible()
    {
        LayerMask l = LayerMask.GetMask("Default");
        gameObject.layer = l;
        if(transform.childCount>0)
        {
            for (int i =0;i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.layer = l;
            }
        }
    }
}
