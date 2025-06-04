using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform target;
    [Range(1,20)]public float tiltMultiplier;
    [Range(0, 1)] public float dampning; 
    Vector3 Veclocity = Vector3.zero;
    Vector3 RotationVelocity = Vector3.zero;
    [Range(0, 1)] public float rotationalDampning;
    PinUpCardUI pinUpCardUI;
    private void Start()
    {
        pinUpCardUI = GetComponent<PinUpCardUI>();
    }

    void Update()
    {
        if (!pinUpCardUI.inFocus)
        {
            float delMax = 0;
            delMax = MathF.Abs(target.position.x - transform.position.x) > delMax ? MathF.Abs(target.position.x - transform.position.x) : delMax;
            float deltaX = -(target.position.x - transform.position.x) * (MathF.PI / 180) * tiltMultiplier;
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref Veclocity, dampning);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, deltaX);
        }
        else
        {
            target.position = transform.position;
        }
    }
}
