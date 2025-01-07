using NUnit;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class Teleport : MonoBehaviour
{
    public Transform point;
    public string music;
    public  bool trigger;
    public GameObject Obj;

    public Sprite LocSignSprite; //спрайт таблички с названием локации куда телепортирует

    private MissionManager MM;
    private LocationSign LS;

    void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        LS = GameObject.FindGameObjectWithTag("LocSign").GetComponent<LocationSign>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            Obj = GameObject.FindGameObjectWithTag("Player");
            Obj.transform.position = point.transform.position;
            AudioManager2.instance.PlayMusic(music);
            LS.LocSignSprite = LocSignSprite;
            LS.IsTeleportationCommited = true;
        }
    }

    void OnGUI() //кнопка собрать
    {
        if (trigger) //игрок наступил на объект
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 130, 25), "[E] Отправиться...");
        }
    }
}
