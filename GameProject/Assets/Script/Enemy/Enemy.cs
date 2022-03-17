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
        //ó�� �������ϱ�
        target = Waypoints.points[0];
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
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
