using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaculeTest : MonoBehaviour
{
    public List<Transform> segmentsTransform;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public AnimationCurve curve;
    
    private Vector3[] segmentV;
    private Move player;
    
    private void Start()
    {
        Init();
        segmentV = new Vector3[transform.childCount];
        player = Move.Instance;
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
        for(int i = segmentsTransform.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                segmentsTransform[i].position = transform.position;
                segmentsTransform[i].rotation = targetDir.rotation * transform.rotation;
            }
            else
            {
                segmentsTransform[i].position = Vector3.SmoothDamp(segmentsTransform[i].position, segmentsTransform[i - 1].position + segmentsTransform[0].right * targetDist,
                    ref segmentV[i], smoothSpeed /segmentsTransform.Count-i);
            }
        }
    }
}
