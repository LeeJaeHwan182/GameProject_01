using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBullet : MonoBehaviour
{
    public GameObject bullet;

    public float delay;
    public float delayBetween;

    private void Update()
    {
        if (delay <= 0f)
        {
            GameObject bullets = Instantiate(bullet, transform.position, Quaternion.identity);
            bullets.transform.parent = transform;
            delay = delayBetween;
        }

        delay -= Time.deltaTime;
    }
}
