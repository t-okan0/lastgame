using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleHighScore : MonoBehaviour
{
    public Text ScoreTex;

    void Start()
    {
        ScoreTex.text = "HIGH SCORE : " + PlayerPrefs.GetInt("HIGH SCORE");   
    }

}
