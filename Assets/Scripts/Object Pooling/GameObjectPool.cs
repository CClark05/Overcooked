using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour 
{
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    public GameObject Get()
    {
        if (availableObjects.Count == 0)
        {
            AddToPool();
        }
        var obj = availableObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    private void AddToPool()
    {
        GameObject newObject = Instantiate(prefab);
        newObject.SetActive(false);
        availableObjects.Enqueue(newObject);
        newObject.GetComponent<IGameObjectPooling>().Pool = this;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        availableObjects.Enqueue(obj);
    }
}
