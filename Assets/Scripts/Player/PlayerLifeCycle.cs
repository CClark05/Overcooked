using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeCycle: MonoBehaviour
{
    public Action<Vector2> OnDeath;
    [SerializeField] private Transform spawnPoint;
    public bool isDead  { get; private set; }

    private void Start()
    {
        Hole.OnTrigger += Die;
        SpawnPlayer();
        GetComponent<PlayerAnimation>().OnDeathAnimationDone += SpawnPlayer;
    }

    private void SpawnPlayer()
    {
        transform.position = spawnPoint.position;
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<PlayerMovement>().UnLockMovement();
        isDead = false;
    }

    private void Die(Vector2 holePosition)
    {
        if (GetComponent<PlayerMovement>().isDashing) return;
        isDead = true;
        
        if (PlayerInteraction.Instance.HasKitchenObject())
        {
            KitchenObject kitchenObject= PlayerInteraction.Instance.GetKitchenObject();
            if (PlateKitchenObject.IsPlate(kitchenObject, out PlateKitchenObject plate))
            {
                SinkInteract.Instance.AddCleanPlate();
            }
        }
        GetComponent<PlayerMovement>().LockMovement();
        OnDeath?.Invoke(GetComponent<PlayerMovement>().GetCurrentDirection());
    }
}
