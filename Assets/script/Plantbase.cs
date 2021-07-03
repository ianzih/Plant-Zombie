using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plantbase : MonoBehaviour
{
    protected BG currbg;
    public float hp;

    public float Hp { get => hp; }


    public void Initforplace(BG bg)
    {
        currbg = bg;
        currbg.Currplant = this;
        Oninitforplace();
    }
    protected virtual void Oninitforplace() { }

    public void Hurt(float zombieattackvalue)
    {
        hp -= zombieattackvalue;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
