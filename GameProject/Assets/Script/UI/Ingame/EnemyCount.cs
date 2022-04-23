using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    public GameObject[] enemy;

    public Text enemyCountText;
    private int enemyCount;

    private void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = enemy.Length;
        enemyCountText.text = "Enemy = " + enemyCount;
    }
}
