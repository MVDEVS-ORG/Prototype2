using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public List<DescriptiveObject> DescriptiveLevelObjects = new();
}

[Serializable]
public class DescriptiveObject
{
    public string Name;
    public ObjectCameraVisible GameObject;
    public ObjectInAltScene SecondObject;
    public string FixedDescription;
    public string Speculations;
    public bool PhotographTaken = false;
    public string Path;
    public Vector3 PositionOfCamera;
    public Quaternion RotationOfCamera;
    public List<string> UnlockWords;
}