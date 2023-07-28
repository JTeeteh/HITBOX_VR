using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{

    public TMP_Text ScoreText;
    public static Score Instance;
    int score = 0;
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        ScoreText.text = "POINTS: " + score;
    }

    public void AddPoints()
    {
        score += 20;
        ScoreText.text = "POINTS: " + score;
    }
}
