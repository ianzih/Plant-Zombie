using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManage : MonoBehaviour
{
    private int sun_num;
    public static GameManage connect;
    public Text score;
    public Text live_text;
    public Text time_text;
    private int live = 10;
    private float time = 150;
    public int Sun_num
    {
        get => sun_num;
        set
        {
            sun_num = value;
            UpdateSunNum(sun_num);
        }
    }

    public int Live
    {
        get => live;
        set
        {
            live = value;
            Updatelive(live);
            Getlive(live);
        }
    }

    public float Time { get => time; }

    void Start()
    {
        connect = this;
        Sun_num = 100;
        live_text.text = live.ToString();
    }
    public void UpdateSunNum(int num)
    {
        score.text = num.ToString();
    }
    public void Updatelive(int num)
    {
        live_text.text = num.ToString();
    }
    public void Getlive(int num)
    {
        if (num <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    void Update()
    {
        time = time - UnityEngine.Time.deltaTime;
        time_text.text = ((int)time).ToString();
        if (time <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
