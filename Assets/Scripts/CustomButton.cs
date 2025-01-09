using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image buttonImage;

    public Sprite selectedSprite; // ������ ��� ������
    public Sprite hoverSprite;
    private Sprite originalSprite; // ������������ ������

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        originalSprite = buttonImage.sprite; // ��������� ������������ ������
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<SelectEquipment>().IsSelected)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.GetComponent<SelectEquipment>().IsSelected)
        {
            buttonImage.sprite = selectedSprite;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonImage.sprite = selectedSprite; // ������ ������ �� ���������
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(0.835f, 0.835f, 0.835f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonImage.sprite = originalSprite; // ���������� ������������ ������
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(1f, 1f, 1f);
    }
}
