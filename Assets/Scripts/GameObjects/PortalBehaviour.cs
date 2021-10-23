using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            reset();
            ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.Portal);
        }
    }

    //reset portal properties after returning to the object pool
    private void reset()
    {
        tag = GameObjectTag.DestructibleBrick;
        sr.sprite = defaultSp;
        col.isTrigger = false;
    }
}
