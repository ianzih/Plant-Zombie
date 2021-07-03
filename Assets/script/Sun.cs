using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private float downsunposy;
    private bool from_sky = true;

    // Update sun 的掉落和過幾秒消失
    void Update()
    {
        if (from_sky)//來自天空的 會下墜
        {
            if (transform.localPosition.y <= downsunposy)
            {
                Invoke("sun_disappear", 5);
                return;
            }
            transform.Translate(new Vector3(0, -1, 0));
        }
        else //來自sunflower
        {
            Invoke("sun_disappear", 5);
            return;
        }
    }

    //使sun不見
    private void sun_disappear()
    {
        Destroy(gameObject);
    }

    //初始sun在天空位置
    public void Initsky(float downposy, float sunposx, float sunposy)
    {
        downsunposy = downposy;
        transform.localPosition = new Vector3(sunposx, sunposy, 0);
    }

    //初始sun在sunflower的附近
    public void Initsunflower_sun(float downposy, float sunposx, float sunposy)
    {
        from_sky = false;
        downsunposy = downposy;
        transform.position = new Vector3(sunposx, sunposy, 0);
    }

    //點擊太陽
    void OnMouseDown()
    {
        Debug.Log("CLICK OK");
        GameManage.connect.Sun_num += 50; //點太陽增加多少分
        Destroy(gameObject);
    }
}
