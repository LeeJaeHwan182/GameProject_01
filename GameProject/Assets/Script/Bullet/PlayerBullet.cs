using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10;

    public GameObject impactEffect;

    float bulletSpeed = 15f;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(gameObject);
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
        else if(collision.transform.tag == "Structure")
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(gameObject);
        }
    }

}
