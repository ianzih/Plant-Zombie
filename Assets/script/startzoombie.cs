using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startzoombie : MonoBehaviour
{
    void Update()
    {
        if(this.transform.localScale.x<1.1)
        {
            this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
        }
        
    }
}
