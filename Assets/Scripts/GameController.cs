using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPre;

    public static GameController instance;

    private GameController()
    {
    }
    
    void Start()
    {
        MapGenerator.instance.InitMap();
        Instantiate(playerPre, MapGenerator.instance.getPlayerSpawnPos(), quaternion.identity);
    }

    //making GameController singleton
    public static GameController Instance()
    {
        if (instance == null)
        {
            instance = new GameController();
        }
        return instance;
    }
}
