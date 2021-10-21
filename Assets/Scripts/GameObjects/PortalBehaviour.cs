using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    public Sprite portalSprite;
    private Collider2D col;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameObjectTag.BombEffect))
        {
            sr.sprite = portalSprite;
            col.isTrigger = true;
        }

        if (other.CompareTag(GameObjectTag.Player))
        {
            
        }
    }
}
