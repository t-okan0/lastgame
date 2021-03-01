using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gohelp : MonoBehaviour

{
    public void OnGameStart()
    {
        SceneManager.LoadScene("help");
    }
}