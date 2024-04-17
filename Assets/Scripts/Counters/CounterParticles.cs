using System;
using UnityEngine;


public class CounterParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    private IParticles particles;

    protected void Awake()
    {
        particles = GetComponent<IParticles>();
        if(particles == null) Debug.LogError("No Particle System Interface Found");
    }

    protected void Start() => particles.OnPlayParticles += () =>
    {
        particleSystem.Play();
    };
}