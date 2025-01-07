using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class ItemPickedDestroy: MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D obj) //«Наезд» на объект
	{
		if (obj.transform.tag == "Player")
		{
			Destroy(gameObject); //Удаление объекта с карты
		}
	}
}
