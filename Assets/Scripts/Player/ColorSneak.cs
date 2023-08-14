using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorSneak : MonoSingleton<ColorSneak>
{
    public float radius = 1f; 
    public LayerMask objectLayer;

    public SpriteRenderer visual;

    private void Start()
    {
        visual = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void PickColor(InputAction.CallbackContext context)
    {
        GetColorFromRaycast();
    }
    
    public void GetColorFromRaycast()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0f, objectLayer);
        
        foreach (RaycastHit2D hit in hits)
        {
            SpriteRenderer renderer = hit.collider.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                Color ambientColor = renderer.color;
                visual.color = ambientColor;
            }
        }
    }
}
