using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : Plantbase
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void ani()
    {
        if (Hp < 600 && Hp >= 400) { animator.Play("nut"); }
        if (Hp < 400 && Hp >= 200) { animator.Play("nut1"); }
        if (Hp < 200 && Hp >= 1) { animator.Play("nut2"); }
    }
    protected override void Oninitforplace()
    {
        InvokeRepeating("ani", 0, 1);
        hp = 600.0f;
    }
}
