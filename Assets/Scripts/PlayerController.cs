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
    
    private Animator anim;
    private Rigidbody2D rb2d;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
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
}
