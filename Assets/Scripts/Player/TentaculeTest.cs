using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XR;

public class TentaculeTest : MonoBehaviour
{
    public List<Transform> segmentsTransform;

    public Transform targetDir;
    public Transform target;
    public float targetDist;
    
    [Range(0.1f, 5f)]
    public float smoothSpeed;
    [Range(0.1f, 5f)]
    public float smoothRotationSpeed;
    
    [Range(0.001f, 5f)]
    public float clampRotationSpeed;
    public float rotaClamp;
    
    
    private Vector3[] segmentV;

    private void Start()
    {
        Init();
        segmentV = new Vector3[segmentsTransform.Count];
        
    }

    void Init()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            segmentsTransform.Add(transform.GetChild(i));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Default();
    }

    private void Default()
    {
        for(int i = segmentsTransform.Count - 1; i >=0 ; i--)
        {
            float intensity = (float)(i + 1) / segmentsTransform.Count;
            if (i == 0)
            {
                segmentsTransform[i].position = transform.position;
                segmentsTransform[i].rotation = Quaternion.Slerp(segmentsTransform[i].rotation,
                    targetDir.rotation * transform.rotation, smoothRotationSpeed);
            }
            else
            {
                Vector3 dir = target.position - segmentsTransform[i].position;
                float angle = Vector3.SignedAngle(-segmentsTransform[i].right, dir, Vector3.forward);
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle) * segmentsTransform[i-1].rotation;
                
                segmentsTransform[i].position = Vector3.SmoothDamp(segmentsTransform[i].position, 
                    segmentsTransform[i - 1].position + segmentsTransform[i - 1].right * targetDist,
                    ref segmentV[i], smoothSpeed * ( 1 - intensity));

                if (Quaternion.Angle(segmentsTransform[i].rotation,segmentsTransform[i-1].rotation) > rotaClamp) 
                {
                    segmentsTransform[i].rotation = Quaternion.Slerp(segmentsTransform[i].rotation,segmentsTransform[i-1].rotation, clampRotationSpeed * Time.deltaTime);
                }
                else
                {
                    segmentsTransform[i].rotation = Quaternion.Slerp(segmentsTransform[i].rotation,targetRotation, intensity);
                }
            }
        }
    }
}
