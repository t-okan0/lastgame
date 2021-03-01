using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip getItemSE;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip stampSE;
    [SerializeField] AudioClip hit;

    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCol;
    public attackManager bullet;
    public Transform attackPoint; //bulletを表示する座標の取得(プレイヤ内のattackpointから)
    float speed;
    bool isDead = false;

    public enum DIRECTION_TYPE　// enum = 列挙する
    {
        STOP, RIGHT, LEFT
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    
    
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

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            AttackAnimation();
            audioSource.PlayOneShot(hit);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        { 
            if(isGround()) 
            {
                audioSource.PlayOneShot(jumpSE);
                rigidbody.velocity = new Vector2(0, 6.5f);
            }
        }
    }
    bool isGround()
    {
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;
        Vector3 endStartPoint = transform.position - transform.up * 0.1f;
        return Physics2D.Linecast(leftStartPoint, endStartPoint, blockLayer) || Physics2D.Linecast(rightStartPoint, endStartPoint, blockLayer);
    }
    private void AttackAnimation() 
    {
        animator.SetTrigger("attack");
        attackManager attack = Instantiate<attackManager>(bullet, attackPoint.position, transform.rotation);
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

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Trap")
        {
            PlayerDeath();
        }

        if (collision.gameObject.tag == "Finish")
        {
            gameManager.GameClear();
        }

        if (collision.gameObject.tag == "Item")
        {
            audioSource.PlayOneShot(getItemSE);
            collision.gameObject.GetComponent<itemmanager>().GetItem();
        }

        if (collision.gameObject.tag == "Enemy")
        {    
                PlayerDeath();
        }

    }
    void PlayerDeath() 
    {
        isDead = true; //isDeadをtrueにすることで上のOnTriggerの処理をreturnで締める。
        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.velocity = new Vector2(0, 5f);
        boxCol = GetComponent<BoxCollider2D>(); //やられた後どこにも接触しない
        Destroy(boxCol);
        animator.Play("PlayerDeath");
        gameManager.GameOver();
    }
    
}
