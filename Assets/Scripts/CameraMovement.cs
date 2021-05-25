using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] private Vector3 offset = new Vector3();
    [SerializeField] private float smoothTime = 0.1f;
    private Vector3 targetPos = new Vector3();
    private Vector3 smoothRef;

    private bool isShaking = false;
    private float shakeMag = 0f;
    private Vector3 shakeDir = new Vector3();
    private float shakeTime = 0f;

    public static CameraMovement INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPos = target.position + offset;

        AddShakeOffset();

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothRef, smoothTime);
    }

    public void ShakeCam(Vector3 direction, float magnitude, float duration)
    {
        shakeDir.Set(direction.x, direction.y, 0f);
        shakeMag = magnitude;
        shakeTime = Time.time + duration;
        isShaking = true;
    }

    void AddShakeOffset()
    {
        if (!isShaking || Time.time > shakeTime)
        {
            isShaking = false;
        }
        else
        {
            Vector3 tempVector = shakeDir;
            tempVector *= shakeMag;
            targetPos += tempVector;
        }
    }
}
