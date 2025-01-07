using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    public Image fillBar;
    public float currentHealth;

    private GameManager GM;

    private void Start()
    {
        currentHealth = maxHealth;
        fillBar = GameObject.Find("fill").GetComponent<Image>();
        fillBar.fillAmount = 1;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        fillBar.fillAmount = currentHealth/maxHealth;
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
