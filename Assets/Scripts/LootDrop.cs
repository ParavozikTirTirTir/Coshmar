using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public GameObject Loot;
    public string LootFromWho;
    public bool IsLootExists = false;
    public Vector2 LootFromWhoPosition;
    //public float LootFromWhoPositionX;
    //public float LootFromWhoPositionY;
    //public float LootSizeX = Loot.GetComponent<RectTransform>().sizeDelta.x;
    //public float LootSizeY = Loot.GetComponent<RectTransform>().sizeDelta.y;
    //public float LootPlaceSizeX;
    //public float LootPlaceSizeY;

    public void Awake()
    {
        Loot.SetActive(false);
    }

    public void Update()
    {
        if (GameObject.Find(LootFromWho))
        {
            LootFromWhoPosition = GameObject.Find(LootFromWho).transform.position;
            //LootFromWhoPositionX = GameObject.Find(LootFromWho).transform.position.x;
            //LootFromWhoPositionY = GameObject.Find(LootFromWho).transform.position.y;
            //LootPlaceSizeX = GameObject.Find(LootFromWho).GetComponent<RectTransform>().sizeDelta.x;
            //LootPlaceSizeY = GameObject.Find(LootFromWho).GetComponent<RectTransform>().sizeDelta.y;
        }

        if (!GameObject.Find(LootFromWho) && !IsLootExists)
        {
            Loot.SetActive(true);
            Loot.transform.position = LootFromWhoPosition;

            //Loot.transform.position.x = LootFromWhoPositionX + (LootSizeX - LootPlaceSizeX);
            //Loot.transform.position.y = LootFromWhoPositionY + (LootSizeY - LootPlaceSizeY);
            IsLootExists = true;
        }
    }
}
