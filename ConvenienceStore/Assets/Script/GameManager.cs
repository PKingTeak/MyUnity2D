using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    //싱글톤 사용
    public static GameManager instance;

   
    void Awake()
    {
        if(instance == null)
        {

            instance = this;
        }
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
    }
    [SerializeField]private int score;
    public int Score {get{return score;}}


    [SerializeField]  private int bestScore;
    public int BestScore{get => bestScore;} //최대 점수

    public const string BestScoreKey = "BestScore";

    [SerializeField]
    private TextMeshProUGUI scoreText;


    void Update()
    {
        InttoText();   
    }

    public void AddScore(int _score)
    {
        
        score += _score;
        if(score<0)
        {
            score = 0;
        }
    }

    public void InttoText()
    {
        scoreText.text = score.ToString();


    }

    public void UpdateScore()
    { 
        if(bestScore > Score)
        {   
           PlayerPrefs.SetInt(BestScoreKey,bestScore); //initBestScore

        }


    }



    
}
