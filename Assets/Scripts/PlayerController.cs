using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 0.1f;

    [SerializeField] 
    private int liveCount;
    
    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Color color;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        anim.SetFloat("Horizontal",h);
        anim.SetFloat("Vertical", v);
        
        rb2d.MovePosition(transform.position + new Vector3(h, v) * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            liveCount--;
            StartCoroutine("ShowDamageEffect", 2f);
        }
    }

    IEnumerator ShowDamageEffect(float durationCount)
    {
        for (int i = 0; i < durationCount * 2; i++)
        {
            color.a = 0;
            sr.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = 1;
            sr.color = color;
            yield return new WaitForSeconds(0.25f);
        }
    }
    
}
