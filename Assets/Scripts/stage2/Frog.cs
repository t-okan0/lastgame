using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    Animator animator;
    Rigidbody2D rigidbody;
    float jumpDown = 1.5f;
    float speed;

    public enum DIRECTION_TYPE　// enum = 列挙する
    {
        STOP, RIGHT, LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.RIGHT;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsGround()) 
        {
            ChangeDirection();
        }
    }

    bool IsGround()
    {
        //鼻先から地面までのY軸線を設定することで崖を認識し反転する
        Vector3 startVec = transform.position + transform.right * 4.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 2.5f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    void ChangeDirection()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else
        {
            direction = DIRECTION_TYPE.RIGHT;
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
        
        rigidbody.velocity = new Vector2(speed, 3f);
        if(rigidbody.velocity.y > 0) 
        {
            rigidbody.velocity *= jumpDown;
        }
    }

}
