using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;
    public enum DIRECTION_TYPE　// enum = 列挙する
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    Animator animator;
    new Rigidbody2D rigidbody;
    float speed;
    float jump = 300;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update() //Updateは随時実行される
    {
        float x = Input.GetAxis("Horizontal");

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
                Jump();
            }
            
        }

    }
    void Jump() 
    {
        rigidbody.AddForce(Vector2.up * jump);
    }
    bool IsGround() 
    {
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;
        Vector3 endStartPoint = transform.position - transform.up * 0.1f;
        Debug.DrawLine(leftStartPoint, endStartPoint);
        Debug.DrawLine(rightStartPoint, endStartPoint);
        return Physics2D.Linecast(leftStartPoint, endStartPoint, blockLayer) || Physics2D.Linecast(rightStartPoint, endStartPoint, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap") 
        {
            Debug.Log("die");
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            gameManager.GameClear();
        }
        if (collision.gameObject.tag == "Item") 
        {
            collision.gameObject.GetComponent<itemmanager>().GetItem();
        }
        if (collision.gameObject.tag == "Enemy") 
        {
            enemyManager enemy = collision.gameObject.GetComponent<enemyManager>();
            if (this.transform.position.y + 0.2f > enemy.transform.position.y)
            {
                enemy.DestroyEnemy();
            }
            else
            {
                Destroy(this.gameObject);
                gameManager.GameOver();
            }
        }
    }

    private void FixedUpdate()　//FixedUpdateは定期的に実行される（アップデートをよりなめらかにする？）
    {
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
}
