using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Spell : MonoBehaviour, IPointerDownHandler
{
    public string DEVSpellName;
    public string SpellName;
    public string SpellDescription;

    public TMP_Text SpellNameText;
    public TMP_Text SpellHavingText;
    public TMP_Text SpellDescriptionText;

    private SpellsList SL;
    private Image ObjectVisible;

    void Start()
    {
        SL = GameObject.Find("MagicBookCanvas").GetComponent<SpellsList>();
        ObjectVisible = GameObject.Find(DEVSpellName).GetComponent<Image>();
        ObjectVisible.color = new Color32(100, 100, 100, 255);
        SpellNameText.text = string.Empty;
        SpellHavingText.text = string.Empty;
        SpellDescriptionText.text = string.Empty;
    }

    void Update()
    {
        if (SL.Spells.Contains(SpellName))
        {
            ObjectVisible.color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellNameText.text = SpellName;
        SpellDescriptionText.text = SpellDescription;
        if (SL.Spells.Contains(SpellName))
        {
            SpellHavingText.text = "Получено";
        }
        else
        {
            SpellHavingText.text = "Не получено";
        }
    }
}
