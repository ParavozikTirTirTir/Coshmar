using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    void Start()
    {
       AudioManager2.instance.PlayMusic("music_back");
    }


}
