using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum planttype
{
    sunflower,
    peashooter,
    nut,
    fastpeashooter,
    icepeashooter
}

public class Plantmanage : MonoBehaviour
{
    public GameObject sunflower_plant;
    public GameObject peashooter_plant;
    public GameObject nut_plant;
    public GameObject fastpeashooter_plant;
    public GameObject icepeashooter_plant;
    public static Plantmanage connect;

    void Start()
    {
        connect = this;
    }
    
    public GameObject GetplantType(planttype plant)
    {
        switch (plant)
        {
            case planttype.sunflower:
                return sunflower_plant;
            case planttype.peashooter:
                return peashooter_plant;
            case planttype.nut:
                return nut_plant;
            case planttype.fastpeashooter:
                return fastpeashooter_plant;
            case planttype.icepeashooter:
                return icepeashooter_plant;
        }
        return null;
    }
}
