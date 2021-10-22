using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<Queue<GameObject>> objectPool;

    //private Dictionary<GameObjectType, List<GameObject>> objectPool;
    
    //get prefab according to given type
     // private GameObject getPrefabByType(GameObjectType type)
     // {
     //     foreach (var item in typePrefabs)
     //     {
     //         if (item.type == type)
     //         {
     //             return item.prefab;
     //         }
     //     }
     //     return null;
     // }
    
     // public GameObject GetObject(GameObjectType type)
     // {
     //     GameObject temp = null;
     //     //if game object pool of type has been created 
     //     if (objectPool.ContainsKey(type) == false)
     //     {
     //         objectPool.Add(type, new List<GameObject>());
     //     }
     //
     //     //if the pool has game object left 
     //     if (objectPool[type].Count > 0)
     //     {
     //         int index = objectPool[type].Count - 1;
     //         temp = objectPool[type][index];
     //         objectPool[type].RemoveAt(index);
     //     }
     //     else
     //     {
     //         GameObject prefab = getPrefabByType(type);
     //
     //         if (prefab != null)
     //         {
     //             temp = Instantiate(prefab, transform);
     //         }
     //     }
     //     return temp;
     // }
     // public void ReturnObject(GameObjectType type, GameObject go)
     // {
     //     //if game object of type has an object pool already 
     //     if (objectPool.ContainsKey(type) && objectPool[type].Contains(go) == false)
     //     {
     //         objectPool[type].Add(go);
     //     }
     //     go.SetActive(false);
     // }
    
    private void Initialize()
    {
        objectPool = new List<Queue<GameObject>>();

        for (int i = 0; i < (int)GameObjectType.NUM_OF_TYPES; i++)
        {
            objectPool.Add(new Queue<GameObject>());
        }
    }

    private void AddGameObject(GameObjectType type)
    {
        var temp_go = ObjectFactory.Instance().createGameObject(type);
        objectPool[(int)type].Enqueue(temp_go);
    }

    private GameObject GetGameObject(GameObjectType type)
    {
        GameObject temp_go = null;
        
        if (objectPool[(int)type].Count < 1)
        {
            AddGameObject(type);
        }

        temp_go = objectPool[(int)type].Dequeue();
        temp_go.SetActive(true);
        
        return temp_go;
    }

    private void returnGameObject(GameObject go, GameObjectType type)
    {
        go.SetActive(false);
        objectPool[(int)type].Enqueue(go);
    }
}

