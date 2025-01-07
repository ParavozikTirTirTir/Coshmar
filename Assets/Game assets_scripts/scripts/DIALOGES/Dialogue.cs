using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //Окно диалога
    public GameObject window;
    //Индикатор диалога
    public GameObject indicator;
    //Текст
    public TMP_Text dialogueText;
    //Список строк диалога
    public List<string> dialogues;


    //Скорость написания букв
    public float writingSpeed;
    //Индекс строки диалога
    private int index;
    //Индекс буквы диалога
    private int charIndex;
    //Начало диалога, логическое значение
    private bool started;
    //Следующая строка диалога
    private bool waitForNext;

    public bool NPCMissionDone;
    public GameObject NPC;

    //скрываем окна индикатора и диалога
    private void Awake()
    {
        dialogueText.text = string.Empty;
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    //окно диалога (видимость)
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    //индикатор диалога (видимость)
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }


    //Начало диалога
    public void StartDialogue()
    {
        if (started) //проверка, активности диалога
            return;
        if (NPCMissionDone)
        {
            //Диалог начался
            started = true;
            //делаем онкно диалога видимым
            ToggleWindow(true);
            //прячем индикатор
            ToggleIndicator(false);
            //индект строки диалога, с которой начинаем писать
            GetDialogue(0);
        }
    }

    private void GetDialogue(int i)
    {
        //начинаем диалог с индекса 0
        index = i;
        //Индекс символов
        charIndex = 0;
        //Отчищаем текст диалогового окна, чтобы записать новую строку
        dialogueText.text = string.Empty;
        //Start writing
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        //запуск диалога = ложь
        started = false;
        //отключаем ожидание след. строки
        waitForNext = false;
        //останавливаем Ienumerators
        StopAllCoroutines();
        dialogueText.text = string.Empty;
        //прячем окно
        ToggleWindow(false);
    }
    //Логика написания текста
    IEnumerator Writing() // правила перебора коллекции строк диалога
    {
        //функция, которая позволяет задерживать написание символов
        yield return new WaitForSeconds(writingSpeed);

        //список строк диалога
        string currentDialogue = dialogues[index];
        //пишем тексто по одной букве
        AudioManager2.instance.PlaySFX("char");
        dialogueText.text += currentDialogue[charIndex];
        //изменяем индекс буквы
        charIndex++;
        //Проверка на конец предложения
        if (charIndex < currentDialogue.Length)
        {
            //сколько ждём до написания символа 
            yield return new WaitForSeconds(writingSpeed);
            //продолжаем писать  //это функция, которая может приостановить выполнение и вернуть управление Unity, а затем продолжить работу с того места, на котором остановилась, в следующем кадре.
            StartCoroutine(Writing());
        }
        else
        {
            //переходим на следующую строку
            waitForNext = true;
        }
    }

    private void Update()
    {
        NPCMissionDone = NPC.GetComponent<MissionBot>().MissionDone;

        if (!started)
            return;

        //проверка:  следующая строка и ввод клавиши E
        if (waitForNext && Input.GetKeyDown(KeyCode.E) && NPCMissionDone)
        {
            //переход на следующую строку
            waitForNext = false;
            index++;

            //проверка выхода за индекс строк
            if (index < dialogues.Count)
            {
                //переходим на след. строку
                GetDialogue(index);
            }
            else
            {
                // диалог закончен
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}
