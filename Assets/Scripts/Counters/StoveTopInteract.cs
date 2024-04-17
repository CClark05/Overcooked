using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static CuttingBoardInteract;

public class StoveTopInteract : BaseCounter, IHasProgressBar, IParticles
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public States state;
    }
    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;


    [SerializeField] private CookingRecipeSO[] recipeArray;
    private float cookProgress;
    private float burnProgress;
    private CookingRecipeSO kitchenObjectRecipe;
    public event Action OnPlayParticles;
    public UnityEvent<Color> OnFoodCooked;
    public UnityEvent OnFoodAboutToBurn;
    public enum States
    {
        Idle,
        Cooking,
        Cooked,
        Burnt
    }
    private States state = States.Idle;

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case States.Idle:

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });


                    break;
                case States.Cooking:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });

                    cookProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        percentProgress = cookProgress / kitchenObjectRecipe.cookTime
                    });
                    if (cookProgress > kitchenObjectRecipe.cookTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(kitchenObjectRecipe.cooked, this);
                        GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
                        burnProgress = 0;
                        OnPlayParticles?.Invoke();
                        OnFoodCooked?.Invoke(new Color(162/255f, 38/255f, 51/255f));
                        state = States.Cooked;
                    }

                    break;
                case States.Cooked:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });
                    burnProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        percentProgress = burnProgress / kitchenObjectRecipe.burnTime
                    });
                    if (burnProgress > kitchenObjectRecipe.burnTime * 0.7f)
                    {
                        OnFoodAboutToBurn?.Invoke();
                        OnFoodAboutToBurn = null;
                    }
                    if (burnProgress > kitchenObjectRecipe.burnTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(kitchenObjectRecipe.burned, this);
                        GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
                        state = States.Burnt;
                    }

                    break;
                case States.Burnt:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        percentProgress = 1
                    });
                    break;
            }
        }
    }

    public override void Interact()
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().GetKitchenSO()))
            {
                kitchenObjectRecipe = FindRecipe(player.GetKitchenObject().GetKitchenSO());
                player.GetKitchenObject().SetParent(this);
                GetKitchenObject().transform.localPosition = GetComponent<CounterVisual>().GetFodVisual().localPosition;
                cookProgress = 0;
                state = States.Cooking;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    percentProgress = 0
                });
                OnStateChanged?.Invoke(this, new StoveTopInteract.OnStateChangedEventArgs
                {
                    state = States.Idle
                });
                GetKitchenObject().SetParent(player);
                state = States.Idle;
            }
            else
            {
                if (PlateKitchenObject.IsPlate(player.GetKitchenObject(), out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                        {
                            percentProgress = 0
                        });
                        OnStateChanged?.Invoke(this, new StoveTopInteract.OnStateChangedEventArgs
                        {
                            state = States.Idle
                        });
                        state = States.Idle;
                    }

                }
            }
        }
    }
    private bool HasRecipe(KitchenObjectSO input)
    {
        if (FindRecipe(input) != null)
        {
            return true;
        }
        return false;
    }
    private CookingRecipeSO FindRecipe(KitchenObjectSO input)
    {
        foreach (CookingRecipeSO recipe in recipeArray)
        {
            if (recipe.raw == input)
            {
                return recipe;
            }
        }
        return null;
    }

    
}