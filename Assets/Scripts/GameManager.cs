using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private GameObject gameover;
    private GameObject gameclear;
    private GameObject scoreText;
    [SerializeField] AudioClip clearSE;
    [SerializeField] AudioClip overSE;
    AudioSource audioSource;
    private int score = 0;

    public void Start()
    {
        this.gameover = GameObject.Find("gameover");
        this.gameclear = GameObject.Find("gameclear");
        this.scoreText = GameObject.Find("scoreText");
        audioSource = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        this.gameover.GetComponent<Text>().text = "Game Over...";
        audioSource.PlayOneShot(overSE);
        Invoke("RestartScene", 3f);
    }

    public void GameClear()
    {
        this.gameclear.GetComponent<Text>().text = "Game Clear!";
        audioSource.PlayOneShot(clearSE);
        Invoke("RestartScene", 3f);
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
    public void AddScore()
    {
        score += 50;
        this.scoreText.GetComponent<Text>().text = "Score" + this.score;
    }
}