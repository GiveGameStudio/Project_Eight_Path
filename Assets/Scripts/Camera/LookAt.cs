using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform Look;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(Look);
    }
}

