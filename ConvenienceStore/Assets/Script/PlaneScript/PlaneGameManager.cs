using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneGameManager : MonoBehaviour
{
    
    static PlaneGameManager instance;
    private int curscore = 0;

    UIManager uiManger;
    public UIManager uIManager { get { return uiManger; } }
    public static PlaneGameManager Instance { get { return instance; } }


    //점수 저장
    public static readonly string PlaneBestScore;

    private int planeBestScore;
    
    private void Awake()
    {
        instance = this;
        uiManger = FindObjectOfType<UIManager>();
    }


   public void GameOver()
    {

        uiManger.SetRestart();
        Debug.Log("게임 오버");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void AddScore(int score)
    {
        curscore += score;
        uiManger.UpdateScore(curscore);

        

    }

    public void UpdateScore()
    {
        planeBestScore = PlayerPrefs.GetInt(PlaneBestScore);
        if (planeBestScore < curscore)
        {
            PlayerPrefs.SetInt(PlaneBestScore, curscore);
        }
        
    }

    
}
