using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject explosionEffect;
    private int range;
    public void Init(int range, int delayTime)
    {
        this.range = range;
        StartCoroutine("ExplosionDelay", delayTime);
    }

    IEnumerator ExplosionDelay(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(explosionEffect, transform.position, quaternion.identity);
        SpawnExplosion(Vector2.left);
        SpawnExplosion(Vector2.right);
        SpawnExplosion(Vector2.up);
        SpawnExplosion(Vector2.down);
        Destroy(gameObject);
    }

    private void SpawnExplosion(Vector2 explosionDirection)
    {
        for (int i = 1; i <= range; i++)
        {
            Vector2 pos = (Vector2)transform.position + explosionDirection * i;

            //stop spawning explosion effect once hits the hard brick
            if (MapGenerator.instance.isHardBrick(pos)) break;
            Instantiate(explosionEffect, pos, quaternion.identity);
        }
    }
}
