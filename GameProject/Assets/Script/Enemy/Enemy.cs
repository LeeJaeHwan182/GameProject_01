using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10f;

    public float startHealth = 100;
    private float health;

    public int value = 50;

    [Header("Unity Stuff")]
    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        //처음 갈곳구하기
        target = Waypoints.points[0];
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;

        Destroy(gameObject);
    }

    private void Update()
    {
        // 거리구하기
        Vector3 dir = target.position - transform.position;
        // 거리이동
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // 다음타겟정하기
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
