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
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject hardBrick, destructibleBrick;
    
    [SerializeField]
    private int colCoordinate;
    
    [SerializeField]
    private int rowCoordinate;

    [SerializeField] 
    private int destructibleBrickNum;
    
    private List<Vector2> emptyLocationList = new List<Vector2>();
    private void Awake()
    {
        GenerateHardWall();
        GetAllEmptyLocations();
        GenerateDestructibleWall();
    }

    private void GenerateHardWall()
    {
        for (int i = -colCoordinate; i <= colCoordinate; i += 2)
        {
            for (int j = -rowCoordinate; j <= rowCoordinate; j += 2)
            {
                BrickInstantiate(hardBrick, new Vector2(i, j));
            }
        }
        
        for (int i = -(colCoordinate + 2); i <= colCoordinate + 2; ++i)
        {
            BrickInstantiate(hardBrick,new Vector2(i, -(rowCoordinate + 2)));
            BrickInstantiate(hardBrick, new Vector2(i, rowCoordinate + 2));
        }

        for (int j = -(rowCoordinate + 2); j <= rowCoordinate + 2; ++j)
        {
            BrickInstantiate(hardBrick, new Vector3(-(colCoordinate + 2), j));
            BrickInstantiate(hardBrick, new Vector3(colCoordinate + 2, j));
        }
    }

    private void BrickInstantiate(GameObject go, Vector2 vec) 
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
    }

    private void GenerateDestructibleWall()
    {
        for (int i = 0; i < destructibleBrickNum; ++i)
        {
            var ran = UnityEngine.Random.Range(0, emptyLocationList.Count);
            BrickInstantiate(destructibleBrick, emptyLocationList[ran]);
        }
    }
}
