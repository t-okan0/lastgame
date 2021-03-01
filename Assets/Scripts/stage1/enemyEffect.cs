using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEffect : MonoBehaviour
{
    public void OnCompleteAnimation() 
    {
        Destroy(this.gameObject);
    }
}
