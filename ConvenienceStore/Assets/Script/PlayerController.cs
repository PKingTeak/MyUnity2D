using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.CompareTag("Unit"))
            { 
                listManager.Interact(collision.gameObject); //��ȣ�ۿ�
                
            }

            if(collision.CompareTag("Door"))
            {
                collision.GetComponent<Door>().Open();

            }
        }
    }







}
