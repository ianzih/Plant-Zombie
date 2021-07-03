using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icepeashooter : Plantbase
{
    public GameObject bullet_load;
    private float attackCD = 1.0f;
    private bool canattack = true;

    //子彈攻擊力
    private int pea_attackval = 20;
    //子彈移動值
    private Vector3 creatBulletOffsetPos = new Vector2(0.36f, 0.211f);
    protected override void Oninitforplace()
    {
        hp = 200.0f;
        InvokeRepeating("Attack", 0, 0.15f);
    }
    private void Attack()
    {
        //取最近的殭屍攻擊
        if (canattack == false) return;
        Zombie zombie = ZombieManager.connect.GetZombieByLineMinDistance((int)currbg.point.y, transform.position);

        //沒殭屍不攻擊
        if (zombie == null) return;
        //在綠地才攻擊
        if (zombie.CurrBGzombie.point.x == 8 && Vector2.Distance(zombie.transform.position, zombie.CurrBGzombie.position) > 1.5f) return;
        //殭屍在左邊不攻擊
        if (transform.position.x > zombie.transform.position.x) return;

        icebullet icebullet_ = GameObject.Instantiate<GameObject>(bullet_load, transform.position + creatBulletOffsetPos, Quaternion.identity, transform).GetComponent<icebullet>();
        icebullet_.init_bullet(pea_attackval);
        CDEnter();
        canattack = false;
    }
    private void CDEnter()
    {
        StartCoroutine(calCD());
    }
    IEnumerator calCD()
    {
        yield return new WaitForSeconds(attackCD);
        canattack = true;
    }
}
