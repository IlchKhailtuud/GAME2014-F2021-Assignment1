using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/24/2021
//Description : return DestructibleBrick to object pool after explosion
////////////////////////////////////////////////////////////////////////////////////////////////////////

public class DestructibleBrickBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameObjectTag.BombEffect))
        {
            ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.DestructibleBrick);
        }
    }
}
