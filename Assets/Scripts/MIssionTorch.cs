using UnityEngine;
using System.Collections;
public class MissionTorch : MonoBehaviour

{
    private MissionManager MM;
    private SpellsList SL;
    public bool trigger = false;
    private SpriteRenderer TorchOff;
    public GameObject Fire;
    public bool IsOn;
    public GameObject Chest;


    void Start() {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        SL = GameObject.Find("MagicBookCanvas").GetComponent<SpellsList>();
        TorchOff = gameObject.GetComponent<SpriteRenderer>();
        IsOn = false;
        Fire.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D obj) //«Наезд» на объект
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //анти наезд на объект
    {
            if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !IsOn && SL.Spells.Contains("Дар Огненной Птицы")) // если игрок рядом с объектом и нажал Е
        {
            MM.LastAction = "Факел зажжен";
            Fire.SetActive(true);
            IsOn = true;
            Chest.GetComponent<ChestTorch>().NumberTorchesOn += 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !IsOn && !SL.Spells.Contains("Дар Огненной Птицы")) // если игрок рядом с объектом и нажал Е
        {
            MM.LastAction = "Вы не владеете необходимым заклинанием, чтобы зажечь огонь";
        }
    }

    void OnGUI() //кнопка собрать
    {
        if (trigger && !IsOn) //игрок наступил на объект
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] Зажечь");
        }
    }
}
