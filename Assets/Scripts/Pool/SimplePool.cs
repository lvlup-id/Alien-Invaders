using UnityEngine;
using System.Collections.Generic;

public class SimplePool : MonoBehaviour
{
    public GameObject targetPrefab;
    public int amount;
    List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amount; i++)
            CreateNew();
    }

    public GameObject CreateNew()
    {
        GameObject obj = Instantiate(targetPrefab);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj = null;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                obj = pooledObjects[i];
                break;
            }
        }

        if (obj == null) obj = CreateNew();
        return obj;
    }
}