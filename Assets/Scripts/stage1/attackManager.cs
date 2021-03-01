using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackManager : MonoBehaviour
{

    [SerializeField] LayerMask blockLayer;
    [SerializeField] AudioClip breaking;

    float Rspeed = 7;
    new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //向きによって弾の方向が変わる
        var playerPos = GameObject.Find("Player").transform.localScale;

        if(playerPos.x >= 0)
        {
            rigidbody.velocity = transform.right * Rspeed;  //right　＝　red axis(赤軸方向)
        }
        else
        {
            rigidbody.velocity = transform.right * -Rspeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Collisionでは出した瞬間から消去される。フィールドでなくこのオブジェクトにトリガーを付与することで成功
    {
        enemyStatus enemys = collision.gameObject.GetComponent<enemyStatus>();
        AudioSource audioSource = GetComponent<AudioSource>();
        //　敵と接触したらダメージを与え消える
        if (collision.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(breaking, transform.position);  //playClipAtPoint = 指定した場所で鳴らすためオブジェクトのデストロイが関係ない
            enemys.OnDamage();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("delete");
            Destroy(this.gameObject);
        }

    }




}
