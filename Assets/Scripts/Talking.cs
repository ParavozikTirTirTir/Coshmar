using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour
{
    public DialogueField DialogueField;

    public int intimacy;
    public Sprite[] Emotions;
    public string CharName;

    private Canvas DialogueWindow;
    private IsPlayerInDialoge PinD;

    void Start()
    {
        DialogueWindow = GetComponent<Canvas>();
        DialogueWindow.enabled = false;
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
    }

    void Update()
    {
        if (PinD.InDialoge)
        {
            DialogueWindow.enabled = true;
            DialogueField.StartDialogue();
        }
        else
        {
            DialogueWindow.enabled = false;
            DialogueField.EndDialogue();
        }
    }
}
