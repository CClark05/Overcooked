using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem dashParticles;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        playerMovement.OnDash += (Vector2 direction) => CreateDashParticles();
    }
    private void CreateDashParticles()
    {
        dashParticles.Play();
    }

}
