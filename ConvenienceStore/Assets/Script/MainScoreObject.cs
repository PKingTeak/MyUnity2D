using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScoreObject : MonoBehaviour
{
    public TextMeshProUGUI PlaneBestScore;
    public TextMeshProUGUI FriendBestScore;

    private static readonly string PlaneBestScoreKey = "PlaneBestScore";
    private static readonly string FriendBestScoreKey = "FriendBestScore";

    private int planescore;
    private int friendscore;

    private void Start()
    {

        planescore = PlayerPrefs.GetInt(PlaneBestScoreKey);
        friendscore = PlayerPrefs.GetInt(FriendBestScoreKey);

        PlaneBestScore.text = planescore.ToString();
        FriendBestScore.text = friendscore.ToString();


    }
}
