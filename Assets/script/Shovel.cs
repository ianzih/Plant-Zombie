using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shovel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool useshovel;
    public Transform shovelimg;
    public Canvas can;
    public bool Useshovel
    {
        get => useshovel;
        set
        {
            useshovel = value;

            if (useshovel)
            {
                shovelimg.localRotation = Quaternion.Euler(0, 0, 45);
            }
            else
            {
                shovelimg.localRotation = Quaternion.Euler(0, 0, 0);
                shovelimg.transform.position = new Vector3(135, 395, 0); ;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 0);
    }
    //點擊 代表你要建立物件來移動
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Useshovel == false)
        {
            Useshovel = true;
        }
    }
    void Update()
    {
        if (Useshovel)
        {
            Vector2 screenPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, Input.mousePosition, null, out screenPoint);
            screenPoint = new Vector2(screenPoint.x - (can.GetComponent<RectTransform>().rect.width) / 2, screenPoint.y - (can.GetComponent<RectTransform>().rect.height) / 2);

            shovelimg.position = screenPoint;
            //刪除左鍵
            if (Input.GetMouseButtonDown(0))
            {
                BG bg = BGmanage.connect.GetBG();

                //沒東西
                if (bg.Currplant == null) return;

                //有東西
                if (Vector2.Distance(BGmanage.connect.getBGpointfrommouse(), bg.Currplant.transform.position) < 1.5f)
                {
                    bg.Currplant.Dead();
                    bg.haveplant = false;
                    Useshovel = false;
                }
            }
            //放回右鍵
            if (Input.GetMouseButtonDown(1))
            {
                Useshovel = false;
            }
        }
    }
}
