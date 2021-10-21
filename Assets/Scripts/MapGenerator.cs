////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/2/2021
//Description : Class for procedrually generating map
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MapGenerator: MonoBehaviour
{
    [SerializeField] 
    private GameObject hardBrick, destructibleBrick, portalPre, propPre, enemyPre;
    
    [SerializeField]
    private int colCoordinate;
    
    [SerializeField]
    private int rowCoordinate;

    [SerializeField] 
    private int destructibleBrickNum;

    [SerializeField] 
    private int enemyCount;
    
    private List<Vector2> emptyLocationList = new List<Vector2>();
    private List<Vector2> destructibleBrickList = new List<Vector2>();
    private List<Vector2> hardBrickList = new List<Vector2>();
    
    public void InitMap()
    {
        GenerateHardWall();
        GetAllEmptyLocations();
        GenerateDestructibleWall();
        CreatePortal();
        CreateProp();
        SpawnEnemy();
    }
    
    private void GenerateHardWall()
    {
        for (int i = -colCoordinate; i <= colCoordinate; i += 2)
        {
            for (int j = -rowCoordinate; j <= rowCoordinate; j += 2)
            {
                Vector2 pos = new Vector2(i, j);
                GOInstantiate(hardBrick, pos);
                hardBrickList.Add(pos);
            }
        }
        
        for (int i = -(colCoordinate + 2); i <= colCoordinate + 2; ++i)
        {
            Vector2 pos1 = new Vector2(i, -(rowCoordinate + 2));
            GOInstantiate(hardBrick,pos1);
            hardBrickList.Add(pos1);

            Vector2 pos2 = new Vector2(i, rowCoordinate + 2);
            GOInstantiate(hardBrick, pos2);
            hardBrickList.Add(pos2);
        }

        for (int j = -(rowCoordinate + 2); j <= rowCoordinate + 2; ++j)
        {
            Vector2 pos1 = new Vector2(-(colCoordinate + 2), j);
            GOInstantiate(hardBrick, pos1);
            hardBrickList.Add(pos1);

            Vector2 pos2 = new Vector2(colCoordinate + 2, j);
            GOInstantiate(hardBrick,pos2);
            hardBrickList.Add(pos2);
        }
    }

    private void GOInstantiate(GameObject go, Vector2 vec) 
    {
        Instantiate(go, vec, quaternion.identity);
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
        
        //Save below positions for player spawn
        emptyLocationList.Remove(new Vector2(-(colCoordinate + 1), rowCoordinate + 1));
        emptyLocationList.Remove(new Vector2(-(colCoordinate + 1), rowCoordinate));
        emptyLocationList.Remove(new Vector2(-(colCoordinate), rowCoordinate + 1));
    }

    private void GenerateDestructibleWall()
    {
        for (int i = 0; i < destructibleBrickNum; ++i)
        {
            var ran = UnityEngine.Random.Range(0, emptyLocationList.Count);
            GOInstantiate(destructibleBrick, emptyLocationList[ran]);
            destructibleBrickList.Add(emptyLocationList[ran]);
            emptyLocationList.RemoveAt(ran);
        }
    }

    private void CreatePortal()
    {
        int index = UnityEngine.Random.Range(0, destructibleBrickList.Count);
        GOInstantiate(portalPre, destructibleBrickList[index]);
        destructibleBrickList.RemoveAt(index);
    }

    private void CreateProp()
    {
        int count = UnityEngine.Random.Range(0, 2 + (int)(destructibleBrickList.Count * 0.05f));

        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, destructibleBrickList.Count);
            GOInstantiate(propPre, destructibleBrickList[index]);
            destructibleBrickList.RemoveAt(index);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var ran = UnityEngine.Random.Range(0, emptyLocationList.Count);
            GOInstantiate(enemyPre, emptyLocationList[ran]);
            emptyLocationList.RemoveAt(ran);
        }
    }

    public Vector2 getPlayerSpawnPos()
    {
        return new Vector2(-(colCoordinate + 1), rowCoordinate + 1);
    }
    
    public bool isHardBrick(Vector2 pos)
    {
        return hardBrickList.Contains(pos);
    }
}
