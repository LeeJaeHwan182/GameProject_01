using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolDolRange : MonoBehaviour
{
    [SerializeField]
    [Header("도트데미지")]
    private float Damage;

    [SerializeField]
    [Header("딜레이")]
    private float countdown;

    private float timeBetweenWaves = 0.2f;
    public GameObject Me;
    public GameObject effect;
    public GameObject effectposition;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Dol")
        {
            if (countdown <= 0f)
            {
                Me.GetComponent<Enemy>().TakeDamage(Damage);
                GameObject effectIns = (GameObject)Instantiate(effect, effectposition.transform.position, Quaternion.identity);
                Destroy(effectIns, 2f);
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        }
    }

}
