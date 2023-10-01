using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorSneak : MonoSingleton<ColorSneak>
{
    public float radius = 1f; 
    public LayerMask objectLayer;

    public SpriteRenderer visual;

    public List<LineRenderer> _tentacules;

    private void Start()
    {
        visual = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        
        foreach (GameObject Go in GameObject.FindGameObjectsWithTag("Tentacule"))
        {
            _tentacules.Add(Go.GetComponent<LineRenderer>());
        }
        
    }

    public void PickColor(InputAction.CallbackContext context)
    {
        Debug.Log("Click");
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
                Debug.Log("loop");
                Color ambientColor = renderer.color;
                visual.color = ambientColor;
                
                foreach (LineRenderer Lr in _tentacules)
                {
                    Lr.startColor = ambientColor;
                    Lr.endColor = ambientColor;
                }
            }
        }
    }
}
