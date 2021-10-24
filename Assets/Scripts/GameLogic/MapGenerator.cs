using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/24/2021
//Description : Class for procedrually generating map
////////////////////////////////////////////////////////////////////////////////////////////////////////

public class MapGenerator: MonoBehaviour
{
    [SerializeField]
    private int colCoordinate;
    
    [SerializeField]
    private int rowCoordinate;
    
    private List<Vector2> emptyLocationList = new List<Vector2>();
    private List<Vector2> hardBrickList = new List<Vector2>();
    
    public void InitMap(int propCount, int enemyCount)
    {
        GenerateHardWall();
        GetAllEmptyLocations();
        GenerateDestructibleWall();
        CreatePortal();
        CreateProp(propCount);
        SpawnEnemy(enemyCount);
    }
    
    private void GenerateHardWall()
    {
        for (int i = -colCoordinate; i <= colCoordinate; i += 2)
        {
            for (int j = -rowCoordinate; j <= rowCoordinate; j += 2)
            {
                Vector2 pos = new Vector2(i, j);
                GameObject brick = ObjectManager.Instance().GetGameObject(pos, GameObjectType.HardBrick);
                hardBrickList.Add(pos);
            }
        }
        
        
        //Generate bottom & top line of bricks
        for (int i = -(colCoordinate + 2); i <= colCoordinate + 2; ++i)
        {
            Vector2 pos1 = new Vector2(i, -(rowCoordinate + 2));
            GameObject brick1 = ObjectManager.Instance().GetGameObject(pos1,GameObjectType.HardBrick);
            hardBrickList.Add(pos1);

            Vector2 pos2 = new Vector2(i, rowCoordinate + 2);
            GameObject brick2 = ObjectManager.Instance().GetGameObject(pos2, GameObjectType.HardBrick);
            hardBrickList.Add(pos2);
        }

        //generate left & right line of bricks
        for (int j = -(rowCoordinate + 2); j <= rowCoordinate + 2; ++j)
        {
            Vector2 pos1 = new Vector2(-(colCoordinate + 2), j);
            GameObject brick1 = ObjectManager.Instance().GetGameObject(pos1, GameObjectType.HardBrick);
            hardBrickList.Add(pos1);

            Vector2 pos2 = new Vector2(colCoordinate + 2, j);
            GameObject brick2 = ObjectManager.Instance().GetGameObject(pos2, GameObjectType.HardBrick);
            hardBrickList.Add(pos2);
        }
    }
    
    private void GetAllEmptyLocations()
    {
        for (int i = -(colCoordinate + 1); i <= (colCoordinate + 1); ++i)
        {
            if (math.abs(((-(colCoordinate + 1))) % 2) == math.abs((i % 2)))
            {
                for (int j = -(rowCoordinate + 1); j <= (rowCoordinate + 1); ++j)
                {
                    emptyLocationList.Add(new Vector2(i, j));
                }
            }
            else
            {
                for (int j = -(rowCoordinate + 1); j <= (rowCoordinate + 1); j += 2)
                {
                    emptyLocationList.Add(new Vector2(i,j));
                }
            }
        }
        
        //Save positions below for player spawn
        emptyLocationList.Remove(new Vector2(-(colCoordinate + 1), rowCoordinate + 1));
        emptyLocationList.Remove(new Vector2(-(colCoordinate + 1), rowCoordinate));
        emptyLocationList.Remove(new Vector2(-(colCoordinate), rowCoordinate + 1));
    }

    private void GenerateDestructibleWall()
    {
        int destructibleBrickNum = (int)(emptyLocationList.Count * 0.4f);
        for (int i = 0; i < destructibleBrickNum; ++i)
        {
            var index = UnityEngine.Random.Range(0, emptyLocationList.Count);
            var pos = emptyLocationList[index];
            GameObject brick = ObjectManager.Instance().GetGameObject(pos, GameObjectType.DestructibleBrick);
            emptyLocationList.RemoveAt(index);
        }
    }

    private void CreatePortal()
    {
        var index = UnityEngine.Random.Range(0, emptyLocationList.Count);
        var pos = emptyLocationList[index];
        GameObject portal = ObjectManager.Instance().GetGameObject(pos, GameObjectType.Portal);
        portal.transform.position = emptyLocationList[index];
        emptyLocationList.RemoveAt(index);
    }

    private void CreateProp(int propCount)
    {
        for (int i = 0; i < propCount; i++)
        {
            var index = UnityEngine.Random.Range(0, emptyLocationList.Count);
            var pos = emptyLocationList[index];
            GameObject prop = ObjectManager.Instance().GetGameObject(pos, GameObjectType.Prop);
            emptyLocationList.RemoveAt(index);
        }
    }

    private void SpawnEnemy(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var index = UnityEngine.Random.Range(0, emptyLocationList.Count);
            var pos = emptyLocationList[index];
            GameObject enemy = ObjectManager.Instance().GetGameObject(pos, GameObjectType.Enemy);
            emptyLocationList.RemoveAt(index);
        }
    }
    
    public Vector2 getPlayerSpawnPos()
    {
        return new Vector2(-(colCoordinate + 1), rowCoordinate + 1);
    }
    
    //if certain position has spawned harbrick
    public bool isHardBrick(Vector2 pos)
    {
        return hardBrickList.Contains(pos);
    }
}
