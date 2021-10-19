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

public class MapGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject hardBrick, destructibleBrick, portalPre, propPre;
    
    [SerializeField]
    private int colCoordinate;
    
    [SerializeField]
    private int rowCoordinate;

    [SerializeField] 
    private int destructibleBrickNum;
    
    private List<Vector2> emptyLocationList = new List<Vector2>();
    private List<Vector2> destructibleWallList = new List<Vector2>();
    private void Awake()
    {
        GenerateHardWall();
        GetAllEmptyLocations();
        GenerateDestructibleWall();
        CreatePortal();
        CreateProp();
    }

    private void GenerateHardWall()
    {
        for (int i = -colCoordinate; i <= colCoordinate; i += 2)
        {
            for (int j = -rowCoordinate; j <= rowCoordinate; j += 2)
            {
                GOInstantiate(hardBrick, new Vector2(i, j));
            }
        }
        
        for (int i = -(colCoordinate + 2); i <= colCoordinate + 2; ++i)
        {
            GOInstantiate(hardBrick,new Vector2(i, -(rowCoordinate + 2)));
            GOInstantiate(hardBrick, new Vector2(i, rowCoordinate + 2));
        }

        for (int j = -(rowCoordinate + 2); j <= rowCoordinate + 2; ++j)
        {
            GOInstantiate(hardBrick, new Vector3(-(colCoordinate + 2), j));
            GOInstantiate(hardBrick, new Vector3(colCoordinate + 2, j));
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
    }

    private void GenerateDestructibleWall()
    {
        for (int i = 0; i < destructibleBrickNum; ++i)
        {
            var ran = UnityEngine.Random.Range(0, emptyLocationList.Count);
            GOInstantiate(destructibleBrick, emptyLocationList[ran]);
            destructibleWallList.Add(emptyLocationList[ran]);
            emptyLocationList.RemoveAt(ran);
        }
    }

    private void CreatePortal()
    {
        int index = UnityEngine.Random.Range(0, destructibleWallList.Count);
        GOInstantiate(portalPre, destructibleWallList[index]);
        destructibleWallList.RemoveAt(index);
    }

    private void CreateProp()
    {
        int count = UnityEngine.Random.Range(0, 2 + (int)(destructibleWallList.Count * 0.05f));

        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, destructibleWallList.Count);
            GOInstantiate(propPre, destructibleWallList[index]);
            destructibleWallList.RemoveAt(index);
        }
    }
}
