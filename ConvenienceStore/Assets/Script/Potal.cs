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
        //���� ���� �����ϴ� �÷��̾�� ���� �ı� �� �ʿ�� ���� ������ �״�� �������� �������. 

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(potalName);
    }
    
   
    
}
