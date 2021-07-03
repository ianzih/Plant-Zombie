using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plantbase
{
    //創建sunflower附近的sun
    void creatNewSun()
    {
        Sun sun = GameObject.Instantiate<GameObject>(Skysun.connect.resource_sun, Vector3.zero, Quaternion.identity, transform).GetComponent<Sun>();
        float randeomy = transform.position.y;
        float randeomx = UnityEngine.Random.Range(transform.position.x - 30, transform.position.x + 30) ;
        sun.Initsunflower_sun(randeomy, randeomx , this.transform.position.y);
    }

    //放的初始
    protected override void Oninitforplace()
    {
        hp = 300.0f;
        InvokeRepeating("creatNewSun", 3, 3);
    }
}
