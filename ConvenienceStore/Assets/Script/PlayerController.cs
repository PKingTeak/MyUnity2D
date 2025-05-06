using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    
    [SerializeField] private Transform weaponPivot;

    [SerializeField][Range(0,30)] private float moveSpeed;
    private Vector2 movedir;

    private Camera cam;

    [SerializeField]
    private float range;
    [SerializeField]
    private UnitListManager listManager;

    private AnimationHandler anihandler;


    private bool isConnect = false;
    private GameObject? target; //nullable 선언

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        listManager = GetComponent<UnitListManager>();
        anihandler = GetComponentInChildren<AnimationHandler>();
        cam = Camera.main;    
    }

    private void FixedUpdate()
    {
        rigid.velocity = movedir * moveSpeed;
    }


    private void Update()
    {
        if (isConnect&& Input.GetKeyDown(KeyCode.E))
        {
            listManager.Interact(target); //��ȣ�ۿ�
        }
        Movement();
    }


    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertial = Input.GetAxisRaw("Vertical");
        movedir = new Vector2(horizontal, vertial).normalized;



        if (horizontal != 0)
        {

            sprite.flipX = (horizontal < 0);
        }
        anihandler.Move(movedir);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.CompareTag("Unit"))
            {
            target = collision.gameObject;
            
             isConnect = true;


            }

            if(collision.CompareTag("Door"))
            {
                collision.GetComponent<Door>().Open();

            }

          if (collision.CompareTag("NPC"))
          {
           collision.GetComponent<ToggleUI>().TextON();
          }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            target = null;
            isConnect = false;
        }

        if (collision.CompareTag("NPC"))
        {
            collision.GetComponent<ToggleUI>().TextOff();
        }

        if (collision.CompareTag("Door"))
        {
            collision.GetComponent<Door>().DoorClose();
        }
    }







}
