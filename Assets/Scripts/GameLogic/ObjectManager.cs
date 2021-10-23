using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class ObjectManager
{
    private static ObjectManager instance;
    private List<Queue<GameObject>> objectPool;

    private ObjectManager()
    {
        Initialize();
    }

    public static ObjectManager Instance()
    {
        if (instance == null)
        {
            instance = new ObjectManager();
        }
        return instance;
    }

    private void Initialize()
    {
        objectPool = new List<Queue<GameObject>>();
    
        for (int i = 0; i < (int)GameObjectType.NUM_OF_TYPES; i++)
        {
            objectPool.Add(new Queue<GameObject>());
        }
    }
    
    public void AddGameObject(GameObjectType type)
    {
        var temp_go = ObjectFactory.instance.createGameObject(type);
        objectPool[(int)type].Enqueue(temp_go);
    }
    
    public GameObject GetGameObject(Vector2 spawnPos, GameObjectType type)
    {
        GameObject temp_go = null;
        
        if (objectPool[(int)type].Count < 1)
        {
            AddGameObject(type);
        }
        
        temp_go = objectPool[(int)type].Dequeue();
        temp_go.transform.position = spawnPos;
        temp_go.SetActive(true);
        
        return temp_go;
    }
    
    public void returnGameObject(GameObject go, GameObjectType type)
    {
        go.SetActive(false);
        objectPool[(int)type].Enqueue(go);
    }
}

