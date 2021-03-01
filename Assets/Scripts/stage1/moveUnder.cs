using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUnder : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 positionLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        positionLimit = this.transform.position;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
