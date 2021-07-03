using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGmanage : MonoBehaviour
{
    public static BGmanage connect;
    private List<Vector2> BGlisttmp = new List<Vector2>();
    private List<BG> BGlist = new List<BG>();
    public Canvas can;
    // Start is called before the first frame update
    void Awake()
    {
        connect = this;
        creatBGlistposition();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(getBGpointfrommouse());
        }
    }
    //建網格點
    private void creatBGlistposition()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BGlist.Add(new BG(new Vector2(i, j), transform.position + new Vector3(66.0f * i, 134.0f * j, 0), false));
            }
        }
    }
    //取離滑鼠最近的網格點
    public Vector2 getBGpointfrommouse()
    {
        return GetBG().position;
    }

    //取得滑鼠點擊的此網格屬性
    public BG GetBG()
    {
        float dis = 1000000;
        BG bg = null;
        for (int i = 0; i < BGlist.Count; i++)
        {
            //取滑鼠座標
            Vector2 screenPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, Input.mousePosition, null, out screenPoint);
            screenPoint = new Vector2(screenPoint.x - (can.GetComponent<RectTransform>().rect.width) / 2, screenPoint.y - (can.GetComponent<RectTransform>().rect.height) / 2);
            //Debug.Log(screenPoint);
            if (Vector2.Distance(screenPoint, BGlist[i].position) < dis)
            {
                dis = Vector2.Distance(screenPoint, BGlist[i].position);
                bg = BGlist[i];
            }
        }
        return bg;
    }
    public BG GetBGbyworldpos(Vector2 worldpos)
    {
        float dis = 1000000;
        BG bg = null;
        for (int i = 0; i < BGlist.Count; i++)
        {
            if (Vector2.Distance(worldpos, BGlist[i].position) < dis)
            {
                dis = Vector2.Distance(worldpos, BGlist[i].position);
                bg = BGlist[i];
            }
        }
        return bg;
    }
    //獲得是一個垂直位置的點
    public BG GetBGvertical(int vertical_num)
    {
        for (int i = 0; i < BGlist.Count; i++)
        {
            if (BGlist[i].point == new Vector2(8, vertical_num))
            {
                return BGlist[i];
            }
        }
        return null;
    }
}
