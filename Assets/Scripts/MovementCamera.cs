using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] Transform Camera;
    [SerializeField] float X;
    [SerializeField] float Y;
    [SerializeField] float Xtime;
    [SerializeField] float Ytime;

    void Start()
    {
        transform.DOMoveX(X, Xtime)
             .SetLoops(-1, LoopType.Yoyo)
             .SetEase(Ease.InOutSine);

        transform.DOMoveY(Y, Ytime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

    }


}
