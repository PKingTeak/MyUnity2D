using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    public GameObject StartButton;
    public GameObject menuUI;

    public TextMeshProUGUI BestScore;
    private int bestscore;
    private static readonly string PlaneBestScorekey = "PlaneBestScore";

    private void Start()
    {
        if (scoreText == null)
            Debug.LogError("score text is null");
        bestscore = PlayerPrefs.GetInt(PlaneBestScorekey);
        BestScore.text = bestscore.ToString();
        if (StartButton.activeSelf == false)
        { 
        StartButton.SetActive(true);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetRestart()
    {
        menuUI.SetActive(true);

    }

    public void UpdateBestScore(int score)
    {
        if (bestscore < score)
        {
            bestscore = score;
            PlayerPrefs.SetInt(PlaneBestScorekey, bestscore);
            PlayerPrefs.Save();
        }
            BestScore.text = bestscore.ToString();
    }
}
