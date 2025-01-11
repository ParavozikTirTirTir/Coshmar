using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
[System.Serializable]

public class Romance : MonoBehaviour
{
    //public IsPlayerCanMove PlayerCanMove;
    public bool trigger = false;
    public bool vis;
    public int intimacy;
    public Sprite[] Emotions;
    public string[] Phrases;
    public string[] PhraseEnd1;
    public string[] PhraseEnd2;
    public string[] Answers;
    public string CharName;

    public string CharacterInformation;

    private MissionManager MM;
    private Inventory Inv;
    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;

    private DialogueField DF;
    private Image IconNpcDialogue;
    private TMP_Text CharNameText;

    private GameObject AnswerOption1;
    private GameObject AnswerOption2;


    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        intimacy = 0;
        DF = GameObject.Find("DialogueFieldObj").GetComponent<DialogueField>();
        IconNpcDialogue = GameObject.Find("IconNpcDialogue").GetComponent<Image>();
        CharNameText = GameObject.Find("CharName").GetComponent<TMP_Text>();

        AnswerOption1 = GameObject.Find("ButtonAnswer1");
        AnswerOption2 = GameObject.Find("ButtonAnswer2");
    }

    void OnTriggerStay2D(Collider2D obj) //игрок рядом с НПС
    {
        if (obj.tag == "Player" && obj.GetComponent<PlayerController>().movementSpeed != 0)
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //игрок отошел от НПС
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !vis && OpenInventory.PlayerCanMove) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            AnswerOption1.SetActive(false);
            AnswerOption2.SetActive(false);
            vis = true;
            PinD.InDialoge = true;

            DF.dialogues = Phrases;
            IconNpcDialogue.sprite = Emotions[0];
            CharNameText.text = CharName;

            DF.PhraseEnd1 = PhraseEnd1;
            DF.PhraseEnd2 = PhraseEnd2;
            DF.NpsWhoInDialogue = gameObject;
            DF.Answers = Answers;
        }

        if (vis && DF.IsDialogueEnded)
        {
            AnswerOption1.SetActive(true);
            AnswerOption2.SetActive(true);

            if (DF.ButtonPressed)
            {
                AnswerOption1.SetActive(false);
                AnswerOption2.SetActive(false);
                if (DF.IsDialogueExit)
                {
                    vis = false;
                    PinD.InDialoge = false;

                    DF.IsDialogueExit = false;
                    DF.IsDialogueEnded = false;
                    DF.ButtonPressed = false;
                }
            }
        }
    }

    void OnGUI()
    {
        if (trigger && vis == false && OpenInventory.PlayerCanMove)
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 110, 25), "[Е] Поговорить");
        }
    }
}
   