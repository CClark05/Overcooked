using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem dashParticles;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObjectPool afterImagePool;
    private Vector3 lastAfterImagePosition;
    [SerializeField] private float distanceBetweenAfterImages;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        AfterImage.parent = transform;
    }
    private void Start()
    {
        playerMovement.OnDash += (Vector2 direction) =>
        {
            CreateDashParticles();
            afterImagePool.Get();
            lastAfterImagePosition = transform.position;
        };
    }
    private void Update()
    {
        if (playerMovement.isDashing)
        {
            if(Vector3.Distance(transform.position, lastAfterImagePosition) > distanceBetweenAfterImages)
            {
                afterImagePool.Get();
                lastAfterImagePosition = transform.position;
            }
        }

    }
    private void CreateDashParticles()
    {
        dashParticles.Play();
    }

    private void TeleportEffect()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

}
