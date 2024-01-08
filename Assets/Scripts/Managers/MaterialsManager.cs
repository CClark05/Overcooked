using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    public static MaterialsManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Material selectedMaterial;
    public Material defaultMaterial;
}
