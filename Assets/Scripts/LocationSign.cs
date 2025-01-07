using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationSign : MonoBehaviour
{
    public Sprite LocSignSprite;
    public GameObject Location;
    public Vector3 sign_place;
    public float LocY;
    public bool IsTeleportationCommited;

    void Start()
    {
        LocY = Location.transform.localPosition.y;
        sign_place = Location.transform.localPosition;
        sign_place.y = Location.transform.localPosition.y + LocY;
    }

    void Update()
    {
        // –¿«Ã≈– › –¿Õ¿ Õ≈ Ã≈Õﬂ“‹, »Õ¿◊≈ ﬂ –” » ¬€–¬”

        if (IsTeleportationCommited) //‚˚ÎÂÁ‡ÂÚ Ì‡ ˝Í‡Ì
        {
            sign_place.y -= 100f * Time.deltaTime;
            gameObject.GetComponent<Image>().sprite = LocSignSprite;
            StartCoroutine(SignTimer());
        }
            
        else //ÛÂÁÊ‡ÂÚ Ì‡‚Âı
            sign_place.y += 100f * Time.deltaTime;

        sign_place.y = Mathf.Clamp(sign_place.y, -100 + LocY, 0 + LocY);
        Location.transform.localPosition = sign_place;
    }

    IEnumerator SignTimer()
    {
        yield return new WaitForSeconds(2);
        IsTeleportationCommited = false;
    }
}



//‘”Õ ÷»ﬂ ƒÀﬂ »«Ã≈Õ≈Õ»ﬂ ÷¬≈“¿
//var color = Location.color;
//if (Input.GetKey(KeyCode.C))
//    color.a -= 5f * Time.deltaTime;
//else
//    color.a += 5f * Time.deltaTime;
//color.a = Mathf.Clamp(color.a, 0, 1);
//Location.color = color;
