using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Color color;
    private int directionId;
    private Vector2 directionVec;
    private bool canMove;

    [SerializeField] 
    private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        InitMoveDirection(UnityEngine.Random.Range(0, 4));
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb2d.MovePosition((Vector2)transform.position + (directionVec * speed));
        }
        else
        {
            ChangeMoveDirection();
        }
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
        if (other.CompareTag(GameObjectTag.Enemy))
        {
            color.a = 0.3f;
            spriteRenderer.color = color;
        }

        if (other.CompareTag(GameObjectTag.HardBrick) || other.CompareTag(GameObjectTag.DestructibleBrick))
        {
            //Reset enemy position to prevent it from jittering
            transform.position =
                new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            
            ChangeMoveDirection();
        }

        if (other.CompareTag(GameObjectTag.BombEffect))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GameObjectTag.Enemy))
        {
            color.a = 1.0f;
            spriteRenderer.color = color;
        }
    }

    private void ChangeMoveDirection()
    {
        List<int> directionList = new List<int>();
        
        if (!Physics2D.Raycast(transform.position, Vector2.up, 0.65f, 1 << 6))
        {
            directionList.Add(0);
        }
        
        if (!Physics2D.Raycast(transform.position, Vector2.down, 0.65f, 1 << 6))
        {
            directionList.Add(1);
        }

        if (!Physics2D.Raycast(transform.position, Vector2.left, 0.65f, 1 << 6))
        {
            directionList.Add(2);
        }
        
        if (!Physics2D.Raycast(transform.position, Vector2.right, 0.65f, 1 << 6))
        {
            directionList.Add(3);
        }

        if (directionList.Count > 0)
        {
            canMove = true;
            InitMoveDirection(directionList[UnityEngine.Random.Range(0, directionList.Count)]);
        }
        else
        {
            canMove = false;
        }
    }
}
