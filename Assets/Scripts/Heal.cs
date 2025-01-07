using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Heal : MonoBehaviour
{
    private PlayerStats PS;
    private MissionManager MM;
    public bool trigger = false;
    public float HealAmount;
    public float OverHeal;
    public float RealHeal;

    public Image fillBar;

    void Start()
    {
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        fillBar = GameObject.Find("fill").GetComponent<Image>();
        fillBar.fillAmount = 1;
    }

    void OnTriggerEnter2D(Collider2D obj) //«Наезд» на объект
    {
        if (obj.tag == "HealCollider")
        {
            if (PS.currentHealth == PS.maxHealth)
            {
                MM.LastAction = "Здоровье полное, вылечиться нельзя";
            }

            else
            {
                PS.currentHealth = PS.currentHealth + HealAmount;
                if (PS.currentHealth > PS.maxHealth)
                {
                    OverHeal = PS.maxHealth - PS.currentHealth;
                    PS.currentHealth = PS.maxHealth;
                }
                fillBar.fillAmount = PS.currentHealth / PS.maxHealth;
                RealHeal = HealAmount + OverHeal;
                MM.LastAction = "Здоровье восстановленно на " + RealHeal + " единиц";

                Destroy(gameObject);
            }
        }
    }
}
