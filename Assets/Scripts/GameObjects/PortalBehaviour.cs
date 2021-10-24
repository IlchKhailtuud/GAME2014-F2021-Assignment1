using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/24/2021
//Description : Class for handling portal interaction with bomb effect & the player & wins state
////////////////////////////////////////////////////////////////////////////////////////////////////////

public class PortalBehaviour : MonoBehaviour
{
    public Sprite portalSprite;
    private Collider2D col;
    private SpriteRenderer sr;
    private Sprite defaultSp;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        defaultSp = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameObjectTag.BombEffect))
        {
            tag = GameObjectTag.Portal;
            
            sr.sprite = portalSprite;
            col.isTrigger = true;
        }

        if (other.CompareTag(GameObjectTag.Player))
        {
            if (GameController.instance.getEnemyCount() <= 0)
            {
                //handle win state 
                UnityEngine.SceneManagement.SceneManager.LoadScene("win", LoadSceneMode.Single);
                Reset();
                ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.Portal);
            }
        }
    }

    //reset portal properties after returning to the object pool
    private void Reset()
    {
        tag = GameObjectTag.DestructibleBrick;
        sr.sprite = defaultSp;
        col.isTrigger = false;
    }
}
