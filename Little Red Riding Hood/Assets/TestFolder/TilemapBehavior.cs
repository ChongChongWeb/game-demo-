using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapBehavior : MonoBehaviour
{

    /// <summary>
    /// 地面移动的结束位置
    /// </summary>
    private Transform end;

    /// <summary>
    /// 地面移动的起始位置
    /// </summary>
    private Transform start;

    /// <summary>
    /// 地面的移动速度
    /// </summary>
    public float speed = 0.2f;

    void Start()
    {
        end = GameObject.Find("GroundEndPos").transform;
        start = GameObject.Find("GroundStartPos").transform;
    }

    void FixedUpdate()
    {
        if (transform.position.x > end.position.x) {
            //往左移动
            transform.position -= new Vector3(speed, 0, 0);
        } else {
            //到达结束点
            BackToStart();
        }
    }

    /// <summary>
    /// 回到起始点
    /// </summary>
    void BackToStart()
    {
        /*float yPosOffset = Random.Range(3, 20) * 0.1f; //随机高度偏移
        float xPosOffset = Random.Range(5, 15) * 0.1f; //随机水平偏移
        transform.position = start.position + new Vector3(xPosOffset, yPosOffset, 0);*/
        transform.position = start.position + new Vector3(0, 0, 0);
    }

}
