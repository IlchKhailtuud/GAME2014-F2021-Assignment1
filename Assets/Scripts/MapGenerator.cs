using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject hardbrick;

    [SerializeField] 
    private float xInitPos;

    [SerializeField] 
    private float yInitPos;

    [SerializeField] 
    private int colNum;
    
    [SerializeField]
    private int rowNum;

    private void Awake()
    {
        GenerateHardWall();
    }

    public void InitMap(int col, int row)
    {
        
    }

    private void GenerateHardWall()
    {
        var xOriPos = xInitPos;
        var yOriPos = yInitPos;

        for (int i = 0; i < colNum; ++i)
        {
            for (int j = 0; j < rowNum; ++j)
            {
                Instantiate(hardbrick, new Vector3(xInitPos , yInitPos, 0), Quaternion.identity);
                yInitPos -= 2;
            }
            
            xInitPos += 2;
            yInitPos = yOriPos;
        }

        var xOuterPos = xOriPos - 1;
        var yOuterYPos = yOriPos + 2;
        
        for (int i = 0; i < (colNum * 2 + 1); ++i)
        {
            Instantiate(hardbrick, new Vector3(xOuterPos, yOuterYPos, 0), quaternion.identity);
            xOuterPos += 1;
        }
    }
    
     
}
