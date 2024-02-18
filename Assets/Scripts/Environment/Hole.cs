using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public static Action<Vector2> OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerLifeCycle player)){
            OnTrigger?.Invoke(transform.position);
        }

    }
    /**
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerLifeCycle player))
        {
            OnTrigger?.Invoke(transform.position);
        }
    }
    */
}
