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

    public GameObject WhaleSkills;

    private void Start()
    {
        //ó�� �������ϱ�
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
        // �Ÿ����ϱ�
        Vector3 dir = target.position - transform.position;
        // �Ÿ��̵�
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // ����Ÿ�����ϱ�
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
        if(PlayerStats.Lives > 0)
        {
            GameObject heartsystem = GameObject.Find("Fames");
            heartsystem.GetComponent<HeartSystem>().TakeDamage();
        }
        Destroy(gameObject);
    }

    public void WhaleSkill()
    {
        StartCoroutine(skill());
    }

    IEnumerator skill()
    {
        WhaleSkills.SetActive(true);
        yield return new WaitForSeconds(4f);
        WhaleSkills.SetActive(false);
    }
}
