using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    [SerializeField] 
    private GameObject playerPre;
    private GameObject player;
    
    private PlayerController playerController;
    private MapGenerator mapGenerator;

    private int propCount = 5;
    private int enemyCount = 4;
    private int timeLeft = 90;
    private float timer = 0.0f;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.InitMap(propCount, enemyCount);
        player = Instantiate(playerPre, mapGenerator.getPlayerSpawnPos(), quaternion.identity);
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        countdownTimer();
        UIConroller.instance.UIUpdate(playerController.liveCount, enemyCount, timeLeft);
    }

    public bool isHardBrick(Vector2 pos)
    {
        return mapGenerator.isHardBrick(pos); 
    }

    public int getEnemyCount()
    {
        return enemyCount;
    }

    public void defeatEnemy()
    {
        if (enemyCount <= 0)
        {
            enemyCount = 0;
        }
        enemyCount--;
    }

    private void countdownTimer()
    {
        timer += Time.deltaTime;

        if (timeLeft <= 0)
        {
            Time.timeScale = 0;
        }

        if (timer >= 1.0f)
        {
            timeLeft--;
            timer = 0;
        }
    }
}
