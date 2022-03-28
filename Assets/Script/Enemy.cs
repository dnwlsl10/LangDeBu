using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum eState
{
    Idle,
    Chasing,
    Attacking,
    Die
}
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private eState currentState;
    bool isTure;
    public System.Action OnEnalbe;

    void Start()
    {
        
        this.currentState = eState.Chasing;
        this.agent = this.GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            this.target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
    void Update()
    {
        this.ChangeStae(this.currentState);
    }



    void ChangeStae(eState state)
    {
        switch (state)
        {
            case eState.Idle:
                {
                   
                }
                break;
            case eState.Chasing:
                {
                    if (this.target != null)
                    {
                        agent.isStopped = false;

                        agent.SetDestination(this.target.position);

                        var dir = (this.target.transform.position - this.gameObject.transform.position).sqrMagnitude;
                        if (dir < Mathf.Pow(3, 2))
                        {
                            Debug.Log("test");
                            this.currentState = eState.Attacking;
                          
                        }
                    }
                }
                break;
            case eState.Attacking:
                {
                    this.Attack();

                    var dir = (this.target.transform.position - this.gameObject.transform.position).sqrMagnitude;
                    if (dir > Mathf.Pow(3, 2))
                    {
                        Debug.Log("test");
                        this.currentState = eState.Chasing;

                    }
                }
                break;
            case eState.Die:
                {

                }
                break;
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            agent.speed = 1;
            Debug.Log("test");
            isTure = true;
        }
    }


    //Test
    void Attack()
    {
        agent.isStopped = true;

        if (Input.GetKeyDown(KeyCode.P))
        {
            WeaponMove weaponMove = this.gameObject.GetComponentInChildren<WeaponMove>();
            weaponMove.Attack();
        }
    }
}
