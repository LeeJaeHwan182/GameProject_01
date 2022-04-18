using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject[] hearts;
    private int life;

    private void Start()
    {
        life = hearts.Length;
    }

    public void TakeDamage()
    {
        if(life >= -1)
        {
            life--;
            Destroy(hearts[life].gameObject);
        }
    }
}
