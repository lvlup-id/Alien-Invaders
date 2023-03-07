using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int amount; // initial pooledObject amount
    public List<GameObject> pooledObject;


    private void Start()
    {
        pooledObject = new List<GameObject>();

        for (int i = 0; i < amount; i++) CreateNew();
    }

    protected virtual GameObject CreateNew()
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        pooledObject.Add(obj);
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj = null;
        for (int i = 0; i < pooledObject.Count; i++)
        {
            // check if gameobject inactive
            if (!pooledObject[i].activeInHierarchy)
            {
                obj = pooledObject[i];

                // stop loop
                break;
            }
        }

        if (obj == null)
            obj = CreateNew();

        // obj pasti tidak null
        obj.SetActive(true);
        return obj;
    }

    public void Release(GameObject obj)
    {
        if (pooledObject.Contains(obj))
            obj.SetActive(false);
    }
}
