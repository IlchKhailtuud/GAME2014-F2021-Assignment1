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
        Explode(Vector2.left);
        Destroy(gameObject);
    }

    private void Explode(Vector2 explosionDirection)
    {
        for (int i = 1; i <= range; i++)
        {
            Instantiate(explosionEffect, (Vector2)transform.position + explosionDirection * i, quaternion.identity);
        }
    }
}
