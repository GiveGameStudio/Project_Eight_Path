using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoSingleton<Move>
{
    
    public float speed;
    public float impulseCD;

    private Rigidbody2D rb;
    private Vector2 dir;
    private float actualCD;
    
    [SerializeField] private float rotationSpeed;
    public AnimationCurve rotationCurve;
    private Transform visual;
    private Quaternion targetRotation;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        visual = transform.GetChild(0);
        animator = visual.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        actualCD += Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        RotateSprite();
    }

    public void Impulse(InputAction.CallbackContext context)
    {
        if (actualCD >= impulseCD && context.started)
        {
            rb.AddForce(dir * speed, ForceMode2D.Impulse);
            actualCD = 0;
            animator.SetTrigger("Impulse");
        }
    }

    public void GetInput(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    private void RotateSprite()
    {
        if (dir != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            float step = rotationSpeed * rotationCurve.Evaluate(Mathf.Abs(Quaternion.Angle(visual.rotation, targetRotation))) * Time.deltaTime;
            visual.rotation = Quaternion.Lerp(visual.rotation, targetRotation, step);
        }
    }
}
