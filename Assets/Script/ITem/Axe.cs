using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public Transform lowDamageTarget;
    public Transform highDamageTarget;
    public Enemy enemy;
    public float damage;


/*    private void OnTriggerEnter(Collider other)
    {
        Collider[] highColliders = Physics.OverlapSphere(highDamageTarget.position, 0.3f);
        var playerController = other.gameObject.GetComponent<PlayerController>();

        if (highColliders.Length >0 && other.gameObject.CompareTag("Player") && enemy.isAttacking == true && playerController.isDamageing != true)
        {
            playerController.OnDamage();
            Debug.Log("어택했다.");
        };
    }*/
}
