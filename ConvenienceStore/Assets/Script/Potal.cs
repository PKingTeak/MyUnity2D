using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    public string potalName;

    public void ChangeScene()
    {
        //DontDestroyOnLoad(); 
        //같은 씬에 존재하는 플레이어는 구지 파괴 될 필요는 없기 때문에 그대로 가져가서 사용하자. 
        SceneManager.LoadScene(potalName);
    }
    
   
    
}
