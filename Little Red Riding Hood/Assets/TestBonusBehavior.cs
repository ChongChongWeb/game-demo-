using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBonusBehavior : MonoBehaviour
{
    public void PlayerCollect()
    {
        Destroy(gameObject);
    }
}
