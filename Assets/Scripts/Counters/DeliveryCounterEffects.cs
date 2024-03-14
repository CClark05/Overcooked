using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private void Start()
    {
        DeliveryCounterInteract.OnFoodDelivered += () =>
        {
            particles.Play();
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            particles.Play();
        }
    }
}
