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
        SceneManager.LoadScene(potalName);
    }
    
   
    
}
