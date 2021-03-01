using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemmanager : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void GetItem()
    {
        gm.AddScore();
        Destroy(this.gameObject);
    }
}
