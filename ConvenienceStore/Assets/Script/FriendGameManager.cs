using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FriendGameManager : MonoBehaviour
{

    //싱글톤 사용
    public static FriendGameManager instance;

    public TextMeshProUGUI BestScoreText;
    [SerializeField] private int score;
    public int Score { get { return score; } }
    [SerializeField] private int bestScore;
    public int BestScore { get => bestScore; } //최대 점수

    public const string FBestScoreKey = "FriendBestScore";

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject MenuUI;

    public PlayerController player;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Time.timeScale = 0;
        bestScore = PlayerPrefs.GetInt(FBestScoreKey, 0);
        BestScoreText.text = bestScore.ToString();
        score = 0;

        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            MenuUI.SetActive(true);
            Time.timeScale = 1;
            score = 0;
        }

    }
  




    void Update()
    {
        InttoText();
    }

    public void AddScore(int _score)
    {

        score += _score;
        if (score < 0)
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
        if (bestScore < Score)
        {
            bestScore = score;
            PlayerPrefs.SetInt(FBestScoreKey, bestScore); //initBestScore
            PlayerPrefs.Save();

        }
        BestScoreText.text = bestScore.ToString();


    }

    public void StartGame()
    {
        Time.timeScale = 1;
        MenuUI.SetActive(false);  // 메뉴만 꺼주기
        score = 0;
    }
    public void GameOver()
    {
        
        UpdateScore();
        MenuUI.SetActive(true);
        Time.timeScale = 0;
        player.GetComponent<UnitListManager>().ClearUnits();


    }




}
