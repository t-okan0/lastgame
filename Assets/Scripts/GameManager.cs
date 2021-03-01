using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip clearSE; // SerializeField :フィールド(変数)をシリアライズ(並列化した複数のデータを直列にする)しInspecterにフィールドとして反映。
    [SerializeField] AudioClip overSE;
    [SerializeField] string[] stageName; //stage名
    [System.NonSerialized] public int currentStageNum = 0; //現在のステージ番号（０始まり）:　NonSerialized =　メンバー変数などがシリアル化されない(機密情報などに使う？)

    static public GameManager instance; 
    public Text highScoreText;
    public string key = "HIGH SCORE";
    public int score = 0;
    public int highScore;

    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //キーにあるハイスコアの読込とstring表示
        highScore = PlayerPrefs.GetInt(key, 0);
        GameObject.Find("HIGHSCORE").GetComponent<Text>().text = "HIGH SCORE : " + highScore.ToString();

    }

    void Update() //ハイスコアの更新
    {     
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(key, highScore);
            GameObject.Find("HIGHSCORE").GetComponent<Text>().text = "HIGH SCORE : " + highScore.ToString();
        }
    }

    public void GameOver()
    {
        GameObject.Find("gameover").GetComponent<Text>().text = "Game Over...";
        audioSource.PlayOneShot(overSE);
        Invoke("RestartScene", 3f);
        score = 0;
    }

    public void GameClear()
    {
        GameObject.Find("gameclear").GetComponent<Text>().text = "Game Clear!";
        audioSource.PlayOneShot(clearSE);
        Invoke("NextStage", 3f);
    }
    
    
    public void NextStage()  //次のステージに進む処理
    {
        currentStageNum += 1;   
        StartCoroutine(WaitForLoadScene());　//コルーチン(停止→再実行)を実行
    }
    
    IEnumerator WaitForLoadScene() //シーンの読み込みと待機を行うコルーチン
    {
        yield return SceneManager.LoadSceneAsync(stageName[currentStageNum]); //シーンを非同期で読み込みし、終わるまで待機
                                                                             //LoadSceneAsync = 非同期で読込が終わるまで待機する。他の処理方法で終わる前に次の処理に行くととNullで返される。
    }
    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene(); //GetActiveScene = 現在のシーンを読む
        SceneManager.LoadScene(thisScene.name); 
    }
  
    public void AddScore()
    {
        score += 50;
        GameObject.Find("scoreText").GetComponent<Text>().text = "SCORE : " + score;
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}