using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int directionId;
    private Vector2 directionVec;

    [SerializeField] 
    private float speed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InitMoveDirection(UnityEngine.Random.Range(0, 4));
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition((Vector2)transform.position + (directionVec * speed));
    }

    private void InitMoveDirection(int dir)
    {

        directionId = dir;
        
        switch (dir)
        {
            case 0:
                directionVec = Vector2.up;
                break;
            case 1:
                directionVec = Vector2.down;
                break;
            case 2:
                directionVec = Vector2.left;
                break;
            case 3:
                directionVec = Vector2.right;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        List<int> directionList = new List<int>();
        
        if (Physics2D.Raycast(transform.position, Vector2.up, 1) == false)
        {
            directionList.Add(0);
        }
        
        if (Physics2D.Raycast(transform.position, Vector2.down, 1) == false)
        {
            directionList.Add(1);
        }

        if (Physics2D.Raycast(transform.position, Vector2.left, 1) == false)
        {
            directionList.Add(2);
        }
        
        if (Physics2D.Raycast(transform.position, Vector2.right, 1) == false)
        {
            directionList.Add(3);
        }

        if (directionList.Count != 0)
        {
            InitMoveDirection(directionList[UnityEngine.Random.Range(0, directionList.Count)]);
        }
    }
}
