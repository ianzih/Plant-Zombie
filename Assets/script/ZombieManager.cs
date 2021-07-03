using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject prefab_zombie;
    public static ZombieManager connect;
    private List<Zombie> zombies = new List<Zombie>();
    private int time_creat_zombie = 6;
    public int time_change_zombie = 100;
    public int time_change_zombie_1 = 60;
    public int time_change_zombie_2 = 45;
    void Awake()
    {
        connect = this;
    }
    private void Start()
    {
        StartCoroutine(calCD());
    }
    IEnumerator calCD()
    {
        while (true)
        {
            creat_zomdie();
            yield return new WaitForSeconds(time_creat_zombie);
        }
    }
    private void Update()
    {
        if (GameManage.connect.Time <= time_change_zombie && GameManage.connect.Time > time_change_zombie_1)
        {
            time_creat_zombie = 4;
        }
        if (GameManage.connect.Time <= time_change_zombie_1 && GameManage.connect.Time > time_change_zombie_2)
        {
            time_creat_zombie = 3;
        }
        if (GameManage.connect.Time <= time_change_zombie_2)
        {
            time_creat_zombie = 2;
        }
    }
    private void creat_zomdie()
    {
        Zombie zombie = GameObject.Instantiate<GameObject>(prefab_zombie, new Vector3(390.0f, 0, 0), Quaternion.identity, transform).GetComponent<Zombie>();
        AddZombie(zombie);
        int range = Random.Range(0, 5);
        zombie.zombie_init(range);
    }
    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
    }
    public void removeZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
    }
    public Zombie GetZombieByLineMinDistance(int which_row_num, Vector3 pos)
    {
        Zombie zombie = null;
        float dis = 10000;
        for (int i = 0; i < zombies.Count; i++)
        {
            //Debug.Log(Vector2.Distance(pos, zombies[i].transform.position));
            if (zombies[i].CurrBGzombie.point.y == which_row_num && Vector2.Distance(pos, zombies[i].transform.position) < dis)
            {
                dis = Vector2.Distance(pos, zombies[i].transform.position);
                zombie = zombies[i];
            }
        }
        return zombie;
    }
}
