using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skysun : MonoBehaviour
{
    public GameObject resource_sun;
    public static Skysun connect;
    private float sunPosy = 455;

    //太陽x座標 在這範圍隨機生成
    private float sunMaxPosx = 150f;
    private float sunMinPosx = -300f;
    //太陽y座標 在這範圍隨機生成
    private float sunMaxPosy = 200f;
    private float sunMinPosy = -300f;

    void Start()
    {
        //每秒有幾個陽光生成
        InvokeRepeating("Createsun", 3, 5);
        connect = this;
    }
    void Createsun()
    {
        Sun sun_move = GameObject.Instantiate<GameObject>(resource_sun,Vector3.zero, Quaternion.identity, transform).GetComponent<Sun>();
        float randeomy = UnityEngine.Random.Range(sunMinPosy, sunMaxPosy);
        float randeomx = UnityEngine.Random.Range(sunMinPosx, sunMaxPosx);
        sun_move.Initsky(randeomy, randeomx, sunPosy);
    }
}
