using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        //처음 갈곳구하기
        target = Waypoints.points[0];
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
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
