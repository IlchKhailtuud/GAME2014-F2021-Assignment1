using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    private static ObjectFactory instance;
    
    public List<PrefabType> typePrefabs = new List<PrefabType>();
    private GameController gameController;
    
    private ObjectFactory()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    public static ObjectFactory Instance()
    {
        if (instance == null)
        {
            instance = new ObjectFactory();
        }
        return instance;
    }

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

    public GameObject createGameObject(GameObjectType type)
    {
        GameObject prefab = getPrefabByType(type);
        MonoBehaviour.Instantiate(prefab);
        prefab.transform.parent = gameController.gameObject.transform;
        prefab.SetActive(false);

        return prefab;
    }
}
