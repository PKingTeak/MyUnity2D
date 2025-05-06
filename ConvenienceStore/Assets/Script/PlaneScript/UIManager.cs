using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;

    private void Start()
    {
        if (scoreText == null)
            Debug.LogError("score text is null");

        if (restartText == null)
            Debug.LogError("restart text is null");


        restartText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetRestart()
    { 
      restartText.gameObject.SetActive(true);  
    }
}
