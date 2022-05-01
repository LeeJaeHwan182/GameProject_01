using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolDolTurret : MonoBehaviour
{
    private Transform target;

    private Animator ani;

    bool Destroy = false;

    [Header("Attributes")]
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject RangePrefab;

    private void Start()
    {
        ani = GetComponent<Animator>();
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
        if (Destroy == false)
        {
            if(target == null)
            {
                ani.SetInteger("Pos", 0); //대상없음
                RangePrefab.SetActive(false);
                return;
            }

            ani.SetInteger("Pos", 1);
            RangePrefab.SetActive(true);
        } 
    }

    public void Sell_Upgrade()
    {
        Destroy = true;
        ani.SetInteger("Pos", 2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
