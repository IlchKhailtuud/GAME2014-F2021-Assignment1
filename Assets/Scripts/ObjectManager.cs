using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<PrefabType> typePrefabs = new List<PrefabType>();

    private Dictionary<GameObjectType, List<GameObject>> objectPool 
        = new Dictionary<GameObjectType, List<GameObject>>();

    //get prefab according to given type
    private GameObject getPrefabByType(GameObjectType type)
    {
        foreach (var item in typePrefabs)
        {
            if (item.type == type)
            {
                return item.prefab;
            }
        }
        return null;
    }

    public GameObject GetObject(GameObjectType type)
    {
        GameObject temp = null;
        //if game object pool of type has been created 
        if (objectPool.ContainsKey(type) == false)
        {
            objectPool.Add(type, new List<GameObject>());
        }

        //if the pool has game object left 
        if (objectPool[type].Count > 0)
        {
            int index = objectPool[type].Count - 1;
            temp = objectPool[type][index];
            objectPool[type].RemoveAt(index);
        }
        else
        {
            GameObject prefab = getPrefabByType(type);

            if (prefab != null)
            {
                temp = Instantiate(prefab, transform);
            }
        }
        return temp;
    }

    public void ReturnObject(GameObjectType type, GameObject go)
    {
        //if game object of type has an object pool already 
        if (objectPool.ContainsKey(type) && objectPool[type].Contains(go) == false)
        {
            objectPool[type].Add(go);
        }
        go.SetActive(false);
    }
}

