using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackManager : MonoBehaviour
{
    [SerializeField] AudioClip attackAudio;
    AudioSource audioSource;
    float Rspeed = 7;
    Rigidbody2D rb;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Rspeed; //right　＝　red axis(赤軸方向) 
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyManager enemys = collision.gameObject.GetComponent<enemyManager>();
        if (collision.gameObject.tag == "Enemy") 
        {
            audioSource.PlayOneShot(attackAudio);
            enemys.OnDamage();
            Destroy(this.gameObject);
        }
        else if (gameObject) 
        {
            Destroy(this.gameObject);
        }
    }
}
