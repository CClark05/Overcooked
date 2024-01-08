using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="KitchenObject")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public string objectName;
    public bool isPreppedIngredient;
    public int visualOrder;
}
