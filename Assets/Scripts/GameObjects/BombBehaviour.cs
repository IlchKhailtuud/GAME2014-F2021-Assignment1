using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BombBehaviour : MonoBehaviour
{
    private int range;
    private Action explosionAct;
    public void Init(int range, float delayTime, Action action)
    {
        this.range = range;
        explosionAct = action;
        StartCoroutine("ExplosionDelay", delayTime);
    }

    IEnumerator ExplosionDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        //notify player script that the bomb has exploded
        if (explosionAct != null) explosionAct();
        
        ObjectManager.Instance().GetGameObject(transform.position, GameObjectType.BombEffect);
        SpawnExplosion(Vector2.left);
        SpawnExplosion(Vector2.right);
        SpawnExplosion(Vector2.up);
        SpawnExplosion(Vector2.down);
        
        //ObjectManager.Instance().returnGameObject(gameObject,GameObjectType.Bomb);
        Destroy(gameObject);
    }

    private void SpawnExplosion(Vector2 explosionDirection)
    {
        for (int i = 1; i <= range; i++)
        {
            Vector2 pos = (Vector2)transform.position + explosionDirection * i;
            
            //stop spawning explosion effect once hits the hard brick
            if (GameController.instance.isHardBrick(pos)) break;
            ObjectManager.Instance().GetGameObject(pos, GameObjectType.BombEffect);
        }
    }
}
