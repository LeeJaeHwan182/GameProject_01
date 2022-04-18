using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleGauge : MonoBehaviour
{
    public Image healthBar;
    float maxHealth = 5f;
    public WaveSpawner timer;

    private void Start()
    {
        healthBar = healthBar.GetComponent<Image>();
        timer = timer.GetComponent<WaveSpawner>();
    }

    private void Update()
    {
        healthBar.fillAmount = timer.countdown / maxHealth;
    }
}
