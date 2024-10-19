using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetBehavior : MonoBehaviour
{
    PlayerBehavior player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    /// <summary>
    /// 与地面相交
    /// </summary>
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == 8) {
            player.SetLanded(true);
        }
    }

    /// <summary>
    /// 与地面分离
    /// </summary>
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 8) {
            player.SetLanded(false);
        }
    }
}
