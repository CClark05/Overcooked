using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirection : MonoBehaviour
{
    [SerializeField] private Transform kitchenObjectParent;
    private Vector2 startingPosition;
    private Vector2 direction => GetComponent<IMoves>().Direction;
    private void Awake()
    {
        startingPosition = kitchenObjectParent.transform.localPosition;
    }
    private void Update()
    {
        if (direction == Vector2.zero) return;
        if (direction.x < 0)
        {
            kitchenObjectParent.transform.localPosition = new Vector2(-startingPosition.x, startingPosition.y);
        }
        else
        {
            kitchenObjectParent.transform.localPosition = new Vector2(startingPosition.x, startingPosition.y);
        }
    }


}
