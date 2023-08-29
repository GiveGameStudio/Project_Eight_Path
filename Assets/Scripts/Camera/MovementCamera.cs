using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] Transform Camera;
    [SerializeField] float X;
    [SerializeField] float Y;
    [SerializeField] float Z;
    [SerializeField] float Xtime;
    [SerializeField] float Ytime;
    [SerializeField] float Ztime;

    void Start()
    {
        transform.DOMoveX(X, Xtime).SetEase(Ease.InOutSine);

        transform.DOMoveY(Y, Ytime).SetEase(Ease.InOutSine);

        transform.DOMoveZ(Z, Ztime).SetEase(Ease.InOutSine);
    }
}
