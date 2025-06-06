using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class DetectableObjectsManager : MonoBehaviour
{
    [SerializeField] Camera _photographCamera;
    public LevelObjects _objectInLevel;
    [SerializeField] List<DescriptiveObject> _objects;
    [SerializeField] LayerMask _layerMask;

    private void Update()
    {
        foreach (var obj in _objectInLevel.DescriptiveLevelObjects)
        {
            if (obj.GameObject.IsWithinCamFrustrum(_photographCamera, _layerMask))
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

    public async Task<bool> TakePhoto()
    {
        if(_objects.Count>1 && _objects.Count!=0)
        {
            Debug.LogError("Too many objects in the Photo");
            return false;
        }
        else
        {
            string path = await GameManager.Instance._cameraCapture.TakePhotoAsync(_objects[0].Name);
            _objects[0].PhotographTaken = true;
            _objects[0].PositionOfCamera = _photographCamera.transform.position;
            _objects[0].RotationOfCamera = _photographCamera.transform.rotation;
            _objects[0].Path = path;
            _objects[0].SecondObject.SetVisible();
            return true;
        }
    }
}
