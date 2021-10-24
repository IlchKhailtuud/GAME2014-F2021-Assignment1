using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/24/2021
//Description : Return BombEffect to object pool after bomb explosion
////////////////////////////////////////////////////////////////////////////////////////////////////////

public class BombEffect : MonoBehaviour
{
    private void AnimFinished()
    {
        ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.BombEffect);
    }
}
