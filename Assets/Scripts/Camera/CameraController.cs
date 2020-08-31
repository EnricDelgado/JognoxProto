using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform targetToFollow;
    public float smoothFactor = 1;

    public Vector3 cameraOffset = new Vector3(0, 0, 10);

    private void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = targetToFollow.transform.position + cameraOffset;
        Vector3 targetPosition = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);

        transform.position = targetPosition;

        transform.LookAt(targetPosition);
    }
}
