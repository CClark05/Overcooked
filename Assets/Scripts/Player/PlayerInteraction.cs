using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerInteraction Instance { get; private set; }

    public event EventHandler<onSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class onSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter; 
    }

    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private LayerMask interactLayer;
    private UserInput userInput;
    [SerializeField] private Transform kitchenObjectTransform;
    

    private BaseCounter selectedCounter;
    private Vector3 playerDirection;
    private PlayerMovement playerMovement;

    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one player");
        }
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        userInput = UserInput.Instance;
        userInput.onInteractPressed += UserInput_onInteractPressed;
        userInput.onCutPressed += UserInput_onCutPressed;
        GetComponent<PlayerLifeCycle>().OnDeath += (Vector2 direction) =>
        {
            selectedCounter = null;
            OnSelectedCounterChanged?.Invoke(this, new onSelectedCounterChangedEventArgs
            {
                selectedCounter = this.selectedCounter
            });
        };
    }

    private void UserInput_onCutPressed(object sender, EventArgs e)
    {
        if (!(GameManager.Instance.GetState() == GameManager.States.Playing)) return;
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate();
        }
    }

    private void UserInput_onInteractPressed(object sender, System.EventArgs e)
    {
        if (!(GameManager.Instance.GetState() == GameManager.States.Playing)) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        playerDirection = playerMovement.GetLastUpdatedDirection();
        if(playerMovement.GetCurrentDirection() != Vector3.zero){
            HandleInteraction();
        }
        
    }
    
    private void HandleInteraction()
    {
        if (GetComponent<PlayerLifeCycle>().isDead) return;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, playerDirection, interactDistance, interactLayer);
        if(raycast.collider != null)
        {
            if(raycast.collider.transform.TryGetComponent(out BaseCounter counter))
            {
                if(selectedCounter != counter)
                {
                    SetNewCounter(counter);
                }
                return;
            }
        }
        SetNewCounter(null);

    }
    private void SetNewCounter(BaseCounter counter)
    {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new onSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public Transform getParentTransform()
    {
        return kitchenObjectTransform;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
