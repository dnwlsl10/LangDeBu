using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    Transform pivot;
    Animator ani;
    Rigidbody rb;
  
    public void Start()
    {
        this.pivot = this.GetComponent<Transform>();
        this.ani = this.GetComponent<Animator>();
        this.rb = this.GetComponentInChildren<Rigidbody>();
        this.rb.isKinematic = true;
    }
    public void Attack()
    {
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        this.rb.isKinematic = false;
        int randomNum = Random.RandomRange(0, 6);

        if(randomNum < 3)
        {
            ani.SetTrigger("UpDownSwing");
        }
        else
        {
            ani.SetTrigger("RightLeftSwing");
        }
        
        yield return new WaitForSeconds(1f);
    }
}
