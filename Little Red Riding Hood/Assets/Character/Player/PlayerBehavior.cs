using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    /// <summary>
    /// 跳跃受力
    /// </summary>
    public float jumpForce = 0.2f;
    /// <summary>
    /// 二段跳受力
    /// </summary>
    public float doubleJumpForce = 0.1f;
    private Vector2 jumpVec, doubleJumpVec;
    /// <summary>
    /// 角色底部是否与地面相交
    /// </summary>
    private bool landed;

    /// <summary>
    /// 玩家滑行总时长
    /// </summary>
    public float slideTime = 1.0f;
    /// <summary>
    /// 玩家滑行速度
    /// </summary>
    public float slideSpeed = 1.0f;
    private Vector2 slideVec;

    /// <summary>
    /// 玩家快速下降速度
    /// </summary>
    public float poundSpeed = -1.0f;
    private Vector2 poundVec;

    Rigidbody2D rigid;
    Animator animator;

    /// <summary>
    /// 角色总状态，0默认跑动，1跳跃上升，2下落，3快速下落(下压)，4滑行, 5二段跳
    /// </summary>
    [Tooltip("States: 0-Run, 1-Jumping, 2-Falling, 3-Pound-falling, 4-Sliding, 5-Double jump")]
    public int playerState = 0;

    /// <summary>
    /// 是否已使用二段跳
    /// </summary>
    private bool hasJumpedTwice = false;

    /// <summary>
    /// 控制状态切替的计时
    /// </summary>
    private float stateTimer = 0.0f;


    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        jumpVec = new Vector2(0, jumpForce);
        doubleJumpVec = new Vector2(0, doubleJumpForce);
        slideVec = new Vector2(slideSpeed, 0);
        poundVec = new Vector2(0, poundSpeed);
        playerState = 0;
        stateTimer = 0.0f;
        landed = false;
    }

    void Update()
    {
        animator.SetInteger("Player State", playerState);

        switch (playerState)
        {
            case 0:
                RunningUpdate();
                break;
            case 1:
                JumpingUpdate();
                break;
            case 2:
                FallingUpdate();
                break;
            case 3:
                PoundingUpdate();
                break;
            case 4:
                SlideUpdate();
                break;
            case 5:
                DoubleJumpUpdate();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 与奖励或敌人相交
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if(obj.layer == 9) {
            obj.GetComponent<TestBonusBehavior>().PlayerCollect();
        }
    }

    /// <summary>
    /// 与奖励或敌人分离
    /// </summary>
    void OnTriggerExit2D(Collider2D col)
    {
        
    }
    
    /// <summary>
    /// 跑动状态的更新
    /// </summary>
    void RunningUpdate()
    {
        //重置状态切替计时
        stateTimer = 0.0f;

        if(Input.GetButtonDown("Up")) {
            rigid.velocity = jumpVec;
            playerState = 1;
        }else if(Input.GetButtonDown("Down")) {
            rigid.velocity = slideVec;
            playerState = 4;
        }

        if(rigid.velocity.y < -0.01f) {
            playerState = 2;
        }

        if(landed == true) {
            hasJumpedTwice = false;
        }
    }

    void JumpingUpdate()
    {
        stateTimer += Time.deltaTime;

        if(Input.GetButtonDown("Down")) {
            playerState = 3;
            rigid.velocity = poundVec;
        }

        CheckDoubleJump();

        if(rigid.velocity.y < 0.01f) {
            playerState = 2;
        }

        if(stateTimer >= 0.2f && landed) {
            playerState = 0;
        }
    }

    void FallingUpdate()
    {
        if(Input.GetButtonDown("Down")) {
            playerState = 3;
            rigid.velocity = poundVec;
        }

        CheckDoubleJump();

        if(landed == true) {
            playerState = 0;
        }
    }

    void SlideUpdate()
    {
        stateTimer += Time.deltaTime;

        if(stateTimer >= slideTime) {
            playerState = 0;
            stateTimer = 0.0f;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        /*if(Input.GetButtonDown("Up")) {
            rigid.velocity = jumpVec;
            playerState = 1;
            stateTimer = 0.0f;
        }

        if(rigid.velocity.y < -0.01f) {
            playerState = 2;
        }*/
    }

    void DoubleJumpUpdate()
    {
        if(Input.GetButtonDown("Down")) {
            playerState = 3;
            rigid.velocity = poundVec;
        }

        if(rigid.velocity.y < 0.01f) {
            playerState = 2;
        }
    }

    void CheckDoubleJump()
    {
        if(Input.GetButtonDown("Up") && !hasJumpedTwice) {
            rigid.velocity = doubleJumpVec;
            playerState = 5;
            hasJumpedTwice = true;
        }
    }

    void PoundingUpdate()
    {
        if(landed == true) {
            playerState = 0;
        }
        Debug.Log("POUND");
    }

    public void SetLanded(bool _landed)
    {
        landed = _landed;
    }

}