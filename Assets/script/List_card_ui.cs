using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class List_card_ui : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image maskimg;
    public float CDtime = 2;
    private bool cantake;//此卡片是否可點選 ，來放置到List
    private bool wanttake;//正需要放置
    public int sum_consume;//種植消耗的太陽數，從unity中設定

    public Plantbase plant;
    public GameObject load_plant;//prefab的植物

    public Canvas can;
    public planttype CardPlantType;

    protected BG currbg;//當下植物所在位置

    public static List_card_ui connect;

    //此卡片是否可點選 ，來放置到List
    public bool Cantake
    {
        get => cantake;
        set
        {
            cantake = value;
            if (!cantake)
            {
                maskimg.fillAmount = 1;
                StartCoroutine(calculateCDtime());
            }
        }
    }

    //正要需要放置
    public bool Wanttake
    {
        get => wanttake;
        set
        {
            wanttake = value;
            if (wanttake)
            {
                GameObject pre = Plantmanage.connect.GetplantType(CardPlantType);
                plant = GameObject.Instantiate<GameObject>(pre, Vector3.zero, Quaternion.identity, GameManage.connect.transform).GetComponent<Plantbase>();
            }
            else
            {
                if (plant != null)
                {
                    Destroy(plant.gameObject);
                    plant = null;
                }
            }
        }
    }
    IEnumerator calculateCDtime()
    {
        float calcd = (1 / CDtime) * 0.3f;
        while (maskimg.fillAmount > 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskimg.fillAmount -= calcd;
        }
        Cantake = true;
        maskimg.fillAmount = 0;
    }
    //mouse 碰到圖片會放大和放小
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.05f, 1.05f, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 0);
    }

    //點擊 代表你要建立物件來移動
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Cantake || GameManage.connect.Sun_num < sum_consume) return;
        if (Wanttake == false)
        {
            Wanttake = true;
        }
    }

    void Start()
    {
        maskimg.fillAmount = 0;
        Cantake = true;
        connect = this;
    }

    void Update()
    {
        //正需放置，且放的物件不為空
        if (Wanttake && plant != null)
        {
            Vector2 screenPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, Input.mousePosition, null, out screenPoint);
            screenPoint = new Vector2(screenPoint.x - (can.GetComponent<RectTransform>().rect.width) / 2, screenPoint.y - (can.GetComponent<RectTransform>().rect.height) / 2);

            plant.transform.position = new Vector3(screenPoint.x, screenPoint.y, 0);
           

            BG bg = BGmanage.connect.GetBGbyworldpos(screenPoint); //BG的所有屬性(位置等等)

            //太遠放不到，和此位置沒種植物
            if (bg.haveplant == false && Vector2.Distance(screenPoint, BGmanage.connect.getBGpointfrommouse()) < 25.0f)
            {
                //案左鍵放置
                if (Input.GetMouseButtonDown(0))
                {
                    plant.transform.position = BGmanage.connect.getBGpointfrommouse();
                    plant.Initforplace(bg);
                    plant = null;
                    Wanttake = false;
                    Cantake = false;
                    GameManage.connect.Sun_num -= sum_consume;
                }
            }
            //案右鍵取消
            if (Input.GetMouseButtonDown(1))
            {
                if (plant != null) Destroy(plant.gameObject);
                plant = null;
                Wanttake = false;
            }
        }
    }
}
