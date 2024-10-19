using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyBehavior : MonoBehaviour
{
    private Vector3 playerPos, playerTelePos;
    private Vector3 thisPos, thisTelePos;
    private Transform player, playerTele;
    private int freezeTimer = 0;
    public int freezeTime = 255;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        Transform playerTele = player.Find("teleport point");
    }

    void FixedUpdate()
    {
        freezeTimer --;
    }

    // Update is called once per frame
    void OnMouseDrag()
    {
        if(freezeTimer > 0) {
            return;
        }

        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        Transform playerTele = player.Find("teleport point");
        Debug.Log("TOUCH");
        playerPos = player.position;
        playerTelePos = playerTele.position;
        thisPos = gameObject.transform.position;
        thisTelePos = gameObject.transform.Find("teleport point").position;

        Vector2 move = thisTelePos - playerTelePos;
        player.position = new Vector3(playerPos.x + move.x, playerPos.y + move.y, 0);
        gameObject.transform.position = new Vector3(thisPos.x - move.x, thisPos.y - move.y, 0);

        freezeTimer = freezeTime;
    }
}
