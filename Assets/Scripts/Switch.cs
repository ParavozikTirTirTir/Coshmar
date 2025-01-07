using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public void SceneSwitch()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(1);
    }
}
