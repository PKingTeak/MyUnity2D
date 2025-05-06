using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition  = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); //게임 내에 존재하는 모든 obstacle를 찾아준다. 
        //연산량이 많기 때문에 start나 Awake에서 한번만 연산을 진행하여 찾을때 사용한다. 업데이트에서 사용할시에 프레임 드랍이 발생할 수 있다. 
        obstacleLastPosition = obstacles[0].transform.position;  //첫번째 있는 장애물 위치를 알아야한다. 
        obstacleCount = obstacles.Length; //크기 만큼 

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggerd" + collision.name);

        if (collision.CompareTag("BackGround"))
        {
            float widthofBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthofBgObject * numBgCount;
            collision.transform.position = pos;
            return; // 이걸 다는 이유는 이미 위에서 충돌된 것을 확인했기 때문에 구지 아래 조건문을 판별하지 않아도 된다. 
        }


        Obstacle obstacle = collision.GetComponent<Obstacle>(); //충돌을 했을때 장애물 클래스가 인지 확인하고 다시 위치를 변경해준다. 
        if (obstacle)
        { 
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition,obstacleCount);
        }
    }

}
