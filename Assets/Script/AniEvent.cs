using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEvent : MonoBehaviour
{
    private Animator ani;
    private Enemy enemy;
    private void Start()
    {
        this.enemy = this.GetComponent<Enemy>();
        this.ani = this.GetComponent<Animator>();
    }
    void OnAttack()
    {
        enemy.OnAttack01_Finished();
    }

    void OnAttack01_01()
    {
        enemy.OnAttack01_01_Finished();
    }

    void OnAttackFinished()
    {

    }

    void OnShoutFinished()
    {
       
    }
}
