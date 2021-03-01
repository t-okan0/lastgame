using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    private GameObject scoreText;
    public Text highScoreText;
    public string key = "HIGH SCORE";
    public int score = 0;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("scoreText");
        highScore = PlayerPrefs.GetInt(key, 0);
        highScoreText.text = "HIGH SCORE : " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(score > highScore) 
        {
            highScore = score;
            PlayerPrefs.SetInt(key, highScore);
            highScoreText.text = "HIGH SCORE : " + highScore.ToString();
        }    
    }

    public void AddScore() 
    {
        score += 50;
        scoreText.GetComponent<Text>().text = "SCORE : " + score; 
    }
}
