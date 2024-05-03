using System;
using UnityEngine;


public class TeleporterEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem teleportParticles;
    [SerializeField] private ParticleSystem constantParticles;
    private void Start()
    {
        GetComponent<Teleporter>().OnTeleported += () =>
        {
            teleportParticles.Play();
            constantParticles.Stop();
        };
        GetComponent<TeleporterVisual>().OnTeleporterEnabledAgain += () => constantParticles.Play();
    }
}

        
