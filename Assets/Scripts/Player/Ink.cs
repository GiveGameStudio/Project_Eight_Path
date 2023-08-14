using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ink : MonoBehaviour
{
    private ParticleSystem fx;
    
    // Start is called before the first frame update
    void Start()
    {
        fx = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    public void InkFX(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            fx.Play();
        }

        if (context.canceled)
        {
            fx.Stop();
        }
    }
}
