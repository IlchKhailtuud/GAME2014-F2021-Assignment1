using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPre;

    private MapGenerator mapGennerator;
    
    // Start is called before the first frame update
    void Start()
    {
        mapGennerator = GetComponent<MapGenerator>();
        mapGennerator.InitMap();
        Instantiate(playerPre, mapGennerator.getPlayerSpawnPos(), quaternion.identity);
    }
}
