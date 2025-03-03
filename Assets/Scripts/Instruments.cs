using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Instruments : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public int JumpsAmount;
    public float Attack;
    public float Health;
    public float EnergyConsumptionReduction;

    public Sprite EmptySlotSprite;
    public Object SelectedSword;
    public Object SelectedMolot;
    public Object SelectedChest;
    public Object SelectedRing;

    public float Energy;
    public int Coins;
    public TMP_Text CoinsTextShop;
    public TMP_Text CoinInvText;
    public TMP_Text EnergyInvText;

    public Image SwordPlace;
    public Image MolotPlace;
    public Image ChestPlace;
    public Image RingPlace;

    public Image[] IconSwords; // ��� ����� � ���������
    public Image[] IconMolots;
    public Image[] IconChests;
    public Image[] IconRings;

    //public TMP_Text[] NumberSwords;
    //public TMP_Text[] NumberMolots;
    //public TMP_Text[] NumberChests;
    //public TMP_Text[] NumberRings;

    private MissionManager MM;
    private Inventory Inv;
    private TMP_Text Stats;

    public Object[] SwordsInInventory;
    public Object[] MolotsInInventory;
    public Object[] ChestsInInventory;
    public Object[] RingsInInventory;

    private Sprite SwordPlaceSprite;
    private Sprite MolotPlaceSprite;
    private Sprite ChestPlaceSprite;
    private Sprite RingPlaceSprite;

    private Object EmptyObject;

    private float BasicSpeed;
    private float BasicJumpHeight;
    private int BasicJumpsAmount;
    private float BasicAttack;
    private float BasicHealth;
    private float BasicEnergyConsumptionReduction;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GetComponent<Inventory>();
        Stats = GameObject.Find("PlayerStatsNumbers").GetComponent<TMP_Text>();
        CoinsTextShop = GameObject.Find("CoinsInInventoryShop").GetComponent<TMP_Text>();
        CoinInvText = GameObject.Find("CoinInvText").GetComponent<TMP_Text>();
        EnergyInvText = GameObject.Find("EnergyInvText").GetComponent<TMP_Text>();

    SwordPlaceSprite = SwordPlace.sprite;
        MolotPlaceSprite = MolotPlace.sprite;
        ChestPlaceSprite = ChestPlace.sprite;
        RingPlaceSprite = RingPlace.sprite;

        Speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().movementSpeed;
        JumpHeight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().jumpForce;
        JumpsAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().amountOfJumps;
        Attack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().attack1Damage;
        Health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth;
        EnergyConsumptionReduction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().EnergyConsumptionReduction;

        BasicSpeed = Speed;
        BasicJumpHeight = JumpHeight;
        BasicJumpsAmount = JumpsAmount;
        BasicAttack = Attack;
        BasicHealth = Health;
        BasicEnergyConsumptionReduction = EnergyConsumptionReduction;
    }

    void Update()
    {
        CoinsTextShop.text = Coins.ToString();
        CoinInvText.text = Coins.ToString();
        EnergyInvText.text = Energy.ToString();

        Stats.text = $"{GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().currentHealth} / {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth}" +
            $"\n{GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().attack1Damage}" +
            $"\n{Speed}" +
            $"\n{JumpHeight}" +
            $"\n{JumpsAmount}" +
            $"\n{GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().EnergyConsumptionReduction}%";

        if (SelectedSword.Name != "")
        {
            SwordPlace.sprite = SelectedSword.Sprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().attack1Damage = BasicAttack + SelectedSword.Attack;
        }
        else
        {
            SwordPlace.sprite = SwordPlaceSprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().attack1Damage = BasicAttack;
        }

        if (SelectedMolot.Name != "")
        {
            MolotPlace.sprite = SelectedMolot.Sprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().EnergyConsumptionReduction = SelectedMolot.EnergyReduction;
        }
        else
        {
            MolotPlace.sprite = MolotPlaceSprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().EnergyConsumptionReduction = BasicEnergyConsumptionReduction;
        }

        if (SelectedChest.Name != "")
        {
            ChestPlace.sprite = SelectedChest.Sprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth = BasicHealth + SelectedChest.AdditionalHealth;
        }
        else
        {
            ChestPlace.sprite = ChestPlaceSprite;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth = BasicHealth;
        }

        if (SelectedRing.Name != "")
        {
            RingPlace.sprite = SelectedRing.Sprite;
            if (OpenInventory.PlayerCanMove)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().movementSpeed = BasicSpeed + SelectedRing.AddSpeed;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().jumpForce = BasicJumpHeight + SelectedRing.AddJumpHeight;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().amountOfJumps = BasicJumpsAmount + SelectedRing.AddJumpsAmount;
            }
            Speed = BasicSpeed + SelectedRing.AddSpeed;
            JumpHeight = BasicJumpHeight + SelectedRing.AddJumpHeight;
            JumpsAmount = BasicJumpsAmount + SelectedRing.AddJumpsAmount;
        }
        else
        {
            RingPlace.sprite = RingPlaceSprite;
            if (OpenInventory.PlayerCanMove)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().movementSpeed = BasicSpeed;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().jumpForce = BasicJumpHeight;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().amountOfJumps = BasicJumpsAmount;
            }
            Speed = BasicSpeed;
            JumpHeight = BasicJumpHeight;
            JumpsAmount = BasicJumpsAmount;
        }

        var swords = Inv.ObjectsInInventory.Where(obj => obj.Type == "Sword").ToArray();
        SwordsInInventory = swords;
        for (int i = 0; i < IconSwords.Length; i++)
        {
            if (i < SwordsInInventory.Length)
            {
                IconSwords[i].sprite = SwordsInInventory[i].Sprite;
            }
            else
            {
                IconSwords[i].sprite = EmptySlotSprite;
            }
        }

        var molots = Inv.ObjectsInInventory.Where(obj => obj.Type == "Molot").ToArray();
        MolotsInInventory = molots;
        for (int i = 0; i < IconMolots.Length; i++)
        {
            if (i < MolotsInInventory.Length)
            {
                IconMolots[i].sprite = MolotsInInventory[i].Sprite;
            }
            else
            {
                IconMolots[i].sprite = EmptySlotSprite;
            }
        }

        var chests = Inv.ObjectsInInventory.Where(obj => obj.Type == "Chest").ToArray();
        ChestsInInventory = chests;
        for (int i = 0; i < IconChests.Length; i++)
        {
            if (i < ChestsInInventory.Length)
            {
                IconChests[i].sprite = ChestsInInventory[i].Sprite;
            }
            else
            {
                IconChests[i].sprite = EmptySlotSprite;
            }
        }

        var rings = Inv.ObjectsInInventory.Where(obj => obj.Type == "Ring").ToArray();
        RingsInInventory = rings;
        for (int i = 0; i < IconRings.Length; i++)
        {
            if (i < RingsInInventory.Length)
            {
                IconRings[i].sprite = RingsInInventory[i].Sprite;
            }
            else
            {
                IconRings[i].sprite = EmptySlotSprite;
            }
        }
    }
}
