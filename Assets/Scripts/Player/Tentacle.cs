using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    public Vector3[] segmentV;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        //Wiggle();
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist,
                ref segmentV[i], smoothSpeed + trailSpeed);
        }
        lineRend.SetPositions(segmentPoses);
    }

    //public float wiggleSpeed;
    //public float wiggleMagnitude;
    
    public void Wiggle()
    {
        //targetDir.localRotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
    }
}
