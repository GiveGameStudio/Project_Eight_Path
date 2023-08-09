using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] Transform Camera;

    void Start()
    {
        transform.DOMoveX(2f, 16f)
             .SetLoops(-1, LoopType.Yoyo)
             .SetEase(Ease.InOutSine);

        transform.DOMoveY(12.1f, 8f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

    }


}
