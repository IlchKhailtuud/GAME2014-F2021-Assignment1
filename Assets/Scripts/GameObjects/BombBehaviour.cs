using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject bombEffect;
    
    private int range;
    public void Init(int range, float delayTime)
    {
        this.range = range;
        StartCoroutine("ExplosionDelay", delayTime);
    }

    IEnumerator ExplosionDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(bombEffect, transform.position, quaternion.identity);
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
            if (GameController.instance.isHardBrick(pos)) break;
            Instantiate(bombEffect, pos, quaternion.identity);
        }
    }
}
