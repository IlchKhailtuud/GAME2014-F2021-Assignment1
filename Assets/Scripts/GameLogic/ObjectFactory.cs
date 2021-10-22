using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory: MonoBehaviour
{
    public static ObjectFactory instance;
    
    public List<PrefabType> typePrefabs = new List<PrefabType>();
    private GameController gameController;

    private void Awake()
    {
        instance = this;
        Initialize();
    }
    
    private void Initialize()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }
    
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

    public GameObject createGameObject(GameObjectType type)
    {
        GameObject prefab = getPrefabByType(type);
        Instantiate(prefab);
        //prefab.transform.parent = gameController.gameObject.transform;
        prefab.SetActive(false);

        return prefab;
    }
}
