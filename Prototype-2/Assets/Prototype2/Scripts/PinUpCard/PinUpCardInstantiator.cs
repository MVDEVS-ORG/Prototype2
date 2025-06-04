using System.Collections.Generic;
using UnityEngine;

public class PinUpCardInstantiator : MonoBehaviour
{
    Dictionary<DescriptiveObject,CardReferenceHolder> ObjectsOnBoard = new();
    [SerializeField] CardReferenceHolder PinUpCard;
    private void OnEnable()
    {
        List<DescriptiveObject> temp = GameManager.Instance._detectableObjectsManager._objectInLevel.DescriptiveLevelObjects;
        foreach(var obj in temp)
        {
            if(obj.PhotographTaken && !ObjectsOnBoard.ContainsKey(obj))
            {
                ObjectsOnBoard.Add(obj,Instantiate(PinUpCard, transform));
                ObjectsOnBoard[obj].card.Initialize(obj);
            }
        }
    }
}
