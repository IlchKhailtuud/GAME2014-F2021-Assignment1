using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private void AnimFinished()
    {
        ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.BombEffect);
    }
}
