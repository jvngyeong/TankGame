using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    public Text EnemyCountText;

    static public int leftEnemy;

    void Start()
    {
        leftEnemy = 10;   
    }

    void Update()
    {
        EnemyCountText.text = "남은 적 수 : "  + leftEnemy;
    }
}
