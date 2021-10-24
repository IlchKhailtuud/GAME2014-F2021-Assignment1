using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MapGenerator.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/24/2021
//Description : Class for handling player movement, placing bomb, taking damages & fail state
////////////////////////////////////////////////////////////////////////////////////////////////////////

public class PlayerController : MonoBehaviour
{
    public GameObject bomb;
    
    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Color color;
    
    private bool canTakeDamage = true;

    //player properties
    public int liveCount = 3;
    private float speed = 0.1f;
    private float bombCD = 0.5f;
    private int bombCount = 1;
    private int bombRange = 1;
    private bool canPlaceBomb = true;
    private float delayTime = 1.5f;

    private bool dieAnimFninished = false;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        anim.SetFloat("Horizontal",h);
        anim.SetFloat("Vertical", v);
        
        rb2d.MovePosition(transform.position + new Vector3(h, v) * speed);
        placeBomb();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;
        
        if (other.CompareTag(GameObjectTag.Enemy) || other.CompareTag(GameObjectTag.BombEffect))
        {
            //handle game over state
            if (--liveCount <= 0)
            {
                DieAnim();
            }

            StartCoroutine("showDamageEffect", 2f);
        }
    }

    //make the player sprite flikcer when take damage & set a short time period of invincibility 
    IEnumerator showDamageEffect(float durationCount)
    {
        canTakeDamage = false;
        for (int i = 0; i < durationCount * 2; i++)
        {
            color.a = 0;
            sr.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = 1;
            sr.color = color;
            yield return new WaitForSeconds(0.25f);
        }
        canTakeDamage = true;
    }

    //set a cooldown timer after placing a bomb to prevent player from spamming the bomb button
    IEnumerator startCoolDown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canPlaceBomb = true;
    }
    
    private void placeBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPlaceBomb == true && bombCount > 0)
        {
            AudioEffect.instance.PlayPlaceBomb();
            bombCount--;
            
            GameObject go = Instantiate(bomb, new Vector2(Mathf.RoundToInt(gameObject.transform.position.x),
                Mathf.RoundToInt(gameObject.transform.position.y)), quaternion.identity);
            
            //using Unity.Action to notify player when the bomb explodes
            go.GetComponent<BombBehaviour>().Init(bombRange,delayTime, ()=>
            {
                bombCount++;
            });
            
            canPlaceBomb = false;
            StartCoroutine("startCoolDown", bombCD);
        }
    }
    
    //select status boost by PropType enum
    public void StatusBoost(int prop)
    {
        switch (prop)
        {
           case 0:
               liveCount++;
               break;
           case 1:
               speedBoost();
               break;
           case 2:
               bombCount++;
               break;
           case 3:
               bombRange++;
               break;
        }
    }
    
    private void speedBoost()
    {
        speed += 0.03f;

        if (speed > 1.5f)
            speed = 1.5f;
    }

    public void DieAnim()
    {
        anim.SetTrigger("Die");
    }
    
    //Load end scene after playing die animation
    private void DieAnimFinished()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("end", LoadSceneMode.Single);
    }

    public int getLiveCount()
    {
        return liveCount;
    }
}
