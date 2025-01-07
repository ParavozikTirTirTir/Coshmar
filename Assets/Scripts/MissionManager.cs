using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int Money; // количество денег;
    public Texture2D Coin;
    public Texture2D MissionIcon1; //иконка миссии с приоритетом 1 и 2
    public Texture2D MissionIcon2;
    public Texture2D MissionIcon;
    public string LastAction;
    public bool MissionInformationOnScreen = false;
    public int MissionID;
    private OpenInventory OI;
    private OpenMagicBook MB;
    private OpenCraft OC;

    public List<string> MissionsInProgress = new List<string>();
    public List<string> MissionsPriority = new List<string>();
    public List<string> MissionsInformation = new List<string>();
    public List<string> MissionsObjectName = new List<string>();

    public Texture2D PlayerIcon;

    //public int K_Screen;

    void Start()
    {
        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
        MB = GameObject.Find("MagicBook").GetComponent<OpenMagicBook>();
        OC = GameObject.Find("Craft").GetComponent<OpenCraft>();
    }

    //void Update()
    //{
    //    //K_Screen = (Screen.width * Screen.height) / 40000;
    //}

    void OnGUI()
    {
        if (!OI.OpenInventoryCheck && !MB.OpenBookCheck && !OC.OpenCraftCheck)
        {
            GUI.Label(new Rect(27, 57, 1000, 30), "" + Money);
            GUI.Label(new Rect(5, 55, 25, 25), Coin);
            GUI.Label(new Rect(5, Screen.height - 25, 1000, 25), LastAction);

            foreach (string mission in MissionsInProgress)
            {
                if (MissionsPriority[MissionsInProgress.IndexOf(mission)] == "1")
                {
                    MissionIcon = MissionIcon1;
                }

                if (MissionsPriority[MissionsInProgress.IndexOf(mission)] == "2")
                {
                    MissionIcon = MissionIcon2;
                }

                if (GUI.Button(new Rect(5, 80 + 25 * MissionsInProgress.IndexOf(mission), 30, 30), MissionIcon, "Label") || GUI.Button(new Rect(35, 85 + 25 * MissionsInProgress.IndexOf(mission), 200, 25), mission + "\n", "Label"))
                {
                    MissionInformationOnScreen = true;
                    MissionID = MissionsInProgress.IndexOf(mission);
                    break;
                }

                if (MissionInformationOnScreen == true && MissionID == MissionsInProgress.IndexOf(mission)) // ВЫВОД ИНФОРМАЦИИ О МИССИИ
                {
                    GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), mission);
                    GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 25, 290, 250), MissionsInformation[MissionsInProgress.IndexOf(mission)]);
                    GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 50, 290, 250), "Требуемый предмет: [" + MissionsObjectName[MissionsInProgress.IndexOf(mission)] + "]x1");

                    if (GUI.Button(new Rect((Screen.width - 100) / 2 - 25, (Screen.height - 300) / 2 + 250, 150, 40), "Закрыть"))
                    {
                        MissionInformationOnScreen = false;
                    }
                }
            }
        }
        //GUIStyle style = GUI.skin.GetStyle("Label");
        //style.fontSize = (int)(K_Screen);
    }
}
