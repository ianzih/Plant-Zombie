using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icebullet : MonoBehaviour
{
    private bool isHit = false;
    private Rigidbody2D Rigidbody;

    private int bullet_attackval;
    public void init_bullet(int att_val)
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(Vector2.right * 10000);
        bullet_attackval = att_val;
    }

    void Update()
    {
        if (isHit) return;

        //transform.Rotate(new Vector3(0, 0, -15));
    }
    private void OnTriggerEnter2D(Collider2D collider_zombie)
    {
        if (isHit) return;
        if (collider_zombie.tag == "zombie")
        {
            isHit = true;

            //使殭屍扣血
            collider_zombie.GetComponentInParent<Zombie>().Hurt(bullet_attackval);
            //使殭屍變慢
            collider_zombie.GetComponentInParent<Zombie>().speed_tmp = 4.0f;
            Destroy(gameObject);
        }
    }
}
