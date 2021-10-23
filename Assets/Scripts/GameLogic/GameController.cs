using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPre;
    
    private MapGenerator mapGenerator;
    
    public static GameController instance;
    
    private int propCount = 5;
    public int enemyCount = 4;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.InitMap(propCount, enemyCount);
        Instantiate(playerPre, mapGenerator.getPlayerSpawnPos(), quaternion.identity);
    }

    public bool isHardBrick(Vector2 pos)
    {
        return mapGenerator.isHardBrick(pos);
    }
}
