using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip getItemSE;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip stampSE;
    AudioSource audioSource;
    BoxCollider2D boxCol;

    public enum DIRECTION_TYPE　// enum = 列挙する
    {
        STOP, RIGHT, LEFT
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    
    Animator animator;
    new Rigidbody2D rigidbody;
    float speed;
    bool isDead = false;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update() //Updateは随時実行される
    {   
        //やられた後の操作をreturnで受け付けなくする
        if (isDead) 
        {
            return;
        }

        //GetAxis (Horizontal) = 方向キーの取得
        float x = Input.GetAxis("Horizontal");   
        //アニメーターのParametersのスピードを取得(RunとIdleの偏移条件)し、ｘがマイナスにならないよう//Mathf.Abs　＝　絶対値
        animator.SetFloat("speed", Mathf.Abs(x));　

        if (x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        if (IsGround())
        {
            if (Input.GetKeyDown("space"))
            {
                audioSource.PlayOneShot(jumpSE);
                rigidbody.velocity = new Vector2(0, 6.5f);
            }
            else
            {
                animator.SetBool("Jumping", false);
            }
           
        }

    }
    private void FixedUpdate()　//FixedUpdateは定期的に実行される（アップデートをよりなめらかにする？）
    {
        if (isDead) 
        {
            return;
        }
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }

    bool IsGround() 
    {
        // 敵を上から踏みつけるため主人公の判定部分を作成
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;
        Vector3 endStartPoint = transform.position - transform.up * 0.1f;
        Debug.DrawLine(leftStartPoint, endStartPoint);
        Debug.DrawLine(rightStartPoint, endStartPoint);
        //blockレイヤーを作成し主人公と敵に設定
        return Physics2D.Linecast(leftStartPoint, endStartPoint, blockLayer) || Physics2D.Linecast(rightStartPoint, endStartPoint, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Trap") 
        {
            PlayerDeath();
        }

        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            gameManager.GameClear();
        }
        
        if (collision.gameObject.tag == "Item") 
        {
            audioSource.PlayOneShot(getItemSE);
            collision.gameObject.GetComponent<itemmanager>().GetItem();
        }
        
        if (collision.gameObject.tag == "Enemy") 
        {
            enemyManager enemy = collision.gameObject.GetComponent<enemyManager>();
            if (this.transform.position.y + 0.2f > enemy.transform.position.y)
            {
                //敵の上から接触すると、ｘ,yの分動く(敵の上に一瞬乗っかる)
                audioSource.PlayOneShot(stampSE);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 3);
                enemy.DestroyEnemy();
            }
            else
            {
                //横から接触
                PlayerDeath();
            }
        }
       
    }
    void PlayerDeath() 
    {
        isDead = true; //isDeadをtrueにすることで上のOnTriggerの処理をreturnで締める。
        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.velocity = new Vector2(0, 5f);
        boxCol = GetComponent<BoxCollider2D>();
        Destroy(boxCol);
        animator.Play("PlayerDeath");
        gameManager.GameOver();

    }
    
}
