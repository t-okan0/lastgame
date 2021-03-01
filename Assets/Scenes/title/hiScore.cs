using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiScore : MonoBehaviour
{
    Text gameManager;

    public void Start()
    {
        gameManager = GetComponent<Text>();
    }
}
