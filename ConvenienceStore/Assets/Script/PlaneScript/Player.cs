using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid2D;

    public float flapForce = 0.0f;
    public float forwardSpeed = 0.0f;
    public bool isDead = false;

    float deathCooldown = 1.0f; // 죽음 공간에 머물면 일정 시간이후 사망하는 것
    bool isFlap = false ; //점프를 했는지 확인
    public bool godMode = false;  //디버그 용으로 사용할 것이다. 


    //GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
      //  gameManager = GameManager.Instance;//사실 받아서 사용하지 않아도 된다 그냥 이건 스타일의 차이라고 생각하자. 
      //장점은 있다 메모리를 미묘하게 조금 더 사용하느거 뿐이지만 만약에 player에서도 awake()를 사용하게 되면 꼬일수가 있다. 
        // 내스타일대로 하면 그냥 GameManager.Instace.매서드 이런식으로 그냥 사용하는게 좀더 편한거 같다.  구지 메모리를 복사하여 사용하는 것 보다 그냥 사용하는게 구지 싱글톤 했는데? 라는 생각이다. 


        animator = GetComponentInChildren<Animator>(); //자식 객체의 컴포넌트를 가져올수 있는 함수이다. 
        rigid2D  = GetComponent<Rigidbody2D>();

        if(animator == null)
        {
            Debug.Log("애니메이션이 없습니다.");
        }

        if(rigid2D == null)
        {
            Debug.Log("no rigidBody");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //죽었을때와 죽지 않았을때 처리를 담당할것이다. 
        if(isDead)
        {
            if(deathCooldown <= 0)
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    PlaneGameManager.Instance.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;//델타 타임은 업데이트 내부에서만 사용이 가능하다. 
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButton(0)) //스페이스 및 마우스 좌클릭 
            {
                isFlap  =true;

            }
            //살아있을때 
        }    
    }


    private void FixedUpdate() //물리 관련 처리를 하기 위해 사용하는 함수
    {
        if(isDead)
        {
            return;
        }
        Vector3 velocity =rigid2D.velocity; //가속도
        velocity.x = forwardSpeed;
        if(isFlap) //flap입력을 받아서 그것을 fixedUpdate에서 처리하는 방식이다 업데이트에서는 따로 처리하지 않음.
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rigid2D.velocity = velocity;

        //각도조정
        float angle = Mathf.Clamp((rigid2D.velocity.y*10),-90,90); //제한을 걸어주는 함수이다. 
        transform.rotation = Quaternion.Euler(0,0,angle); //오일러 공식을 사용할 것이다.  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(godMode)
        {
            return;
        }
        if(isDead)
        {
        return;
        }

        isDead = true;
        deathCooldown = 1f;

        animator.SetInteger("IsDie",1); //1로 바꾸면 애니메이션 바꾸는것
        PlaneGameManager.Instance.GameOver();
        
    }
   

}
