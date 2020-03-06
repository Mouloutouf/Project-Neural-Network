using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    public Vector3 cameraLocalPosition;
    public Vector3 localTargetLookAtPosition;

    private Vector3 wantedPos;

    public float posLerpSpeed = 0.02f;
    public float lookLerpSpeed = 0.1f;

    void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        wantedPos = target.TransformPoint(cameraLocalPosition);
        wantedPos.y = cameraLocalPosition.y;

        transform.position = Vector3.Lerp(transform.position, wantedPos, posLerpSpeed);

        Quaternion lookRotation = Quaternion.LookRotation(target.TransformPoint(localTargetLookAtPosition) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookLerpSpeed);
    }

    public virtual void Init()
    {
        instance = this;
    }
}
