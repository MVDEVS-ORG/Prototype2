using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DetectableObjectsManager : MonoBehaviour
{
    [SerializeField] Camera _photographCamera;
    public LevelObjects _objectInLevel;
    [SerializeField] List<DescriptiveObject> _objects;

    private void Update()
    {
        foreach (var obj in _objectInLevel.DescriptiveLevelObjects)
        {
            if (obj.GameObject.IsWithinCamFrustrum(_photographCamera))
            {
                if (!_objects.Contains(obj))
                {
                    _objects.Add(obj);
                }
            }
            else
            {
                if (_objects.Contains(obj))
                {
                    _objects.Remove(obj);
                }
            }
        }
    }

    private void TakePhoto()
    {
        if(_objects.Count>1)
        {
            Debug.LogError("Too many objects in the Photo");
        }
        else
        {
            //call the photo thing here to get path
            _objects[0].PhotographTaken = true;
            _objects[0].positionOfCamera = _photographCamera.transform.position;
            _objects[0].forwardDirection = _photographCamera.transform.forward;
        }
    }
}
