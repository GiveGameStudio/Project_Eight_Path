using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    public float smoothTime = 0.2f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        var position = Move.Instance.transform.position + offset;
        var smoothedPosition  = transform.position;
        Vector3 player = new Vector3(position.x ,position.y, smoothedPosition.z);

        smoothedPosition = Vector3.SmoothDamp(smoothedPosition , player, ref velocity, smoothTime);
        transform.position = smoothedPosition ;
    }
}
