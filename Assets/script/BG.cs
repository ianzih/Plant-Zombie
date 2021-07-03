using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG
{
    //點(0,0)(1,2)...
    public Vector2 point;

    //世界座標
    public Vector2 position;

    //此位置是否有植物
    public bool haveplant;

    private Plantbase currplant;
    public BG(Vector2 point, Vector2 position, bool haveplant)
    {
        this.point = point;
        this.position = position;
        this.haveplant = haveplant;
    }

    public Plantbase Currplant
    {
        get => currplant;
        set
        {
            currplant = value;
            if (currplant == null)
            {
                haveplant = false;
            }
            else
            {
                haveplant = true;
            }
        }
    }
}
