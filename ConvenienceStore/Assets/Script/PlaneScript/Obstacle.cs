using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;
    //어디 위치 시킬 것인가 
    public float holeSizeMin  = 1f;
    public float holeSizeMax = 3f;
    //탑과 바텀 사의 공간의 크기
    public Transform topObejct;
    public Transform bottomObject;
    //나열할 오브젝트들 
    public float widthPadding = 4f;
    //오브젝트를 배치할때 사이 폭을 얼만큼 설정해 줄지 정해주는 변수 
    //즉 첫번재 블록이 생성되고 다음 블록이 생성될때까지의 넓이를 의미하는 변수이다. 
  
    public Vector3 SetRandomPlace(Vector3 lastPosition , int ObstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin,holeSizeMax); //구멍 크기 설정
        float halfHoleSize = holeSize/2; //절반 크기 
        topObejct.localPosition = new Vector3(0,halfHoleSize); 
        bottomObject.localPosition = new Vector3(0,-halfHoleSize);
        //위 아래 반대로 설치되어야 된다. 그래야 공간이 생긴다. 

        Vector3 placePosition = lastPosition + new Vector3(widthPadding,0);
        placePosition.y = Random.Range(lowPosY,highPosY);

        transform.position = placePosition;

        return placePosition;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlaneGameManager.Instance.AddScore(1);
        }
        
        
    }


}
