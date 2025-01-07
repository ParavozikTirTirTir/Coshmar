using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour
{
    public DialogueField DialogueField;

    public int intimacy;
    public Sprite[] Emotions;
    public List<string> Phrases = new List<string>();
    public string CharName;

    private Canvas DialogueWindow;
    private IsPlayerInDialoge PinD;

    // Start is called before the first frame update
    void Start()
    {
        DialogueWindow = GetComponent<Canvas>();
        DialogueWindow.enabled = false;
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
    }

    // Update is called once per frame
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
