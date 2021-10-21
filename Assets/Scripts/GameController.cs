using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPre;

    [SerializeField] 
    private MapGenerator mapGenerator;
    
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.InitMap();
        Instantiate(playerPre, mapGenerator.getPlayerSpawnPos(), quaternion.identity);
    }

    public bool isHardBrick(Vector2 pos)
    {
        return mapGenerator.isHardBrick(pos);
    }
}
