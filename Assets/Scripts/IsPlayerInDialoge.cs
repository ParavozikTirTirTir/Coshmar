using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInDialoge : MonoBehaviour
{
    public bool InDialoge = false;

    void Update()
    {
        if (InDialoge)
        {
            OpenInventory.PlayerCanMove = false;
        }
    }
}
