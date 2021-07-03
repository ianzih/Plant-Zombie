using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private BG currBG;
    private Animator animator;
    private float zombie_speed = 2.5f;
    public float zombieattackvalue = 100.0f;
    private int zombieHP = 180;
    private bool isattack = false;
    private bool dolive = false;
    public static Zombie connect;

    public int tmp;
    public float speed_tmp;
    public BG CurrBGzombie { get => currBG; }
    public BG CurrBG { get => currBG; set => currBG = value; }
    public int ZombieHP { get => zombieHP; }

    public void zombie_init(int which_row)
    {
        animator = GetComponentInChildren<Animator>();
        GetBGbyverticalnum(which_row);
    }
    void Update()
    {
        zombie_move();
    }
    private void Start()
    {
        connect = this;
        if (GameManage.connect.Time <= ZombieManager.connect.time_change_zombie)
        {
            zombieHP = 380;
            zombieattackvalue = 150;
        }
        tmp = zombieHP;
        speed_tmp = zombie_speed;
    }

    //決定殭屍出現在哪一列出現
    private void GetBGbyverticalnum(int vertical_num)
    {
        CurrBG = BGmanage.connect.GetBGvertical(vertical_num);
        this.transform.position = new Vector3(transform.position.x, CurrBG.position.y);
    }

    public void zombie_move()
    {
        if (CurrBG == null) return;
        if (isattack == true) return;

        //當下有植物要攻擊且距離很近，沒的話就移動
        CurrBG = BGmanage.connect.GetBGbyworldpos(transform.position);
        if (CurrBG.haveplant == true
            //&& CurrBG.Currplant.transform.position.x < transform.position.x
            && transform.position.x - CurrBG.Currplant.transform.position.x < 0.3f)
        {
            Attack(CurrBG.Currplant);
            return;
        }
        transform.Translate((new Vector2(-66.0f, 0) * (Time.deltaTime / 1)) / speed_tmp);
        if (tmp == 180)
        {
            if (ZombieHP > 100)
            {
                animator.Play("zombie");
            }
            else if (ZombieHP <= 100 && ZombieHP > 0)
            {
                animator.Play("noHeadZombie1");
            }
        }
        if (tmp == 380)
        {
            if (ZombieHP > 150)
            {
                animator.Play("FlagZombie");
            }
            else if (ZombieHP <= 150 && ZombieHP > 0)
            {
                animator.Play("NoHeadFlagZombie");
            }
        }
        if (transform.position.x <= -450 && dolive == false)
        {
            GameManage.connect.Live -= 1;
            dolive = true;
            ZombieManager.connect.removeZombie(this);
            Destroy(gameObject);
        }
    }

    private void Attack(Plantbase plant)
    {
        isattack = true;
        if (tmp == 180)
        {
            if (ZombieHP > 100)
            {
                animator.Play("ZombieAttack1");
            }
            else
            {
                animator.Play("noHeadZombieAttack1");
            }
        }
        if (tmp == 380)
        {
            if (ZombieHP > 150)
            {
                animator.Play("FlagZombieAttack");
            }
            else
            {
                animator.Play("NoHeadFlagZombieAttack");
            }
        }
        StartCoroutine(doHurt(plant));
    }

    //對植物的傷害
    IEnumerator doHurt(Plantbase plant)
    {
        while (CurrBG.Currplant.Hp > 0)
        {
            plant.Hurt(zombieattackvalue / 5);
            yield return new WaitForSeconds(0.2f);
            if (CurrBG.haveplant == false) break;
        }
        isattack = false;
        CurrBG.haveplant = false;
    }
    IEnumerator dead_ani()
    {      
        animator.Play("ZombieAshes1");
        yield return new WaitForSeconds(0.45f);
        ZombieManager.connect.removeZombie(this);
        Destroy(gameObject);
    }
    //植物對殭屍攻擊
    public void Hurt(int plantattackvalue)
    {
        zombieHP -= plantattackvalue;
        if (ZombieHP <= 0)
        {
            speed_tmp = 1000;
            //死了
            StartCoroutine(dead_ani());
        }
    }
}
