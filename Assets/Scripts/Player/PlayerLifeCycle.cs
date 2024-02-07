using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeCycle: MonoBehaviour
{
    public Action OnDeath;
    [SerializeField] private Transform spawnPoint;
    private PlayerAnimation playerAnimation;
    public bool isDead  { get; private set; }
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    private void Start()
    {
        Hole.OnTrigger += Die;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        isDead = false;
        transform.position = spawnPoint.position;
        GetComponent<PlayerMovement>().UnFreezeInput();
    }

    private void Die(Vector2 holePosition)
    {
        if (GetComponent<PlayerMovement>().isDashing) return;
        OnDeath?.Invoke();
        isDead = true;
        if (PlayerInteraction.Instance.HasKitchenObject())
        {
            Vector2 direction = GetComponent<PlayerMovement>().GetLastUpdatedDirection();
            TileManager.Instance.TryGetBaseCounterFromPosition(holePosition - direction, out BaseCounter counter);
            if (!(counter is FloorInteract))
            {
                Debug.LogError("PlayerLifeCycle.Die()");
                return;
            }
            FloorInteract floor = counter as FloorInteract;
            floor.HasObject();
            if (floor.HasKitchenObject())
            {
                floor.AddToList(floor.GetKitchenObject());
            }
            PlayerInteraction.Instance.GetKitchenObject().SetParent(floor);
            
        }
        GetComponent<PlayerMovement>().FreezeInput(); 
        //playerAnimation.PlayAnimation(PlayerAnimation.Animations.PlayerDeath, SpawnPlayer);
    }
}
