using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolDolTurret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject RangePrefab;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        if (nearstEnemy != null && shortestDistance <= range)
        {
            target = nearstEnemy.transform;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            RangePrefab.SetActive(false);
            return;
        }

        RangePrefab.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
