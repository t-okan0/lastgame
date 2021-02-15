using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemmanager : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void GetItem()
    {
        gameManager.AddScore();
        Destroy(this.gameObject);
    }
}
