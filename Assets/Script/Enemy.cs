using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum eState
{
    Idle,
    Chasing,
    Angry,
    Attacking,
    Ready,
    Die
}
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private eState currentState;
    private Animator ani;
    public System.Action OnEnalbe;
   
    float distance;
    float maxDistance = Mathf.Pow(5,2);
    float attackDistance = Mathf.Pow(2.5f, 2);

    public bool isAttacking = false;
    public bool isReadying = false;
    public bool isChasing = false;
    public bool isIdleing = false;
    public bool isAngry = false;
    void Start()
    {
        this.ani = this.GetComponent<Animator>();
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
                    this.agent.isStopped = false;
                    this.PlayeAnimation(eState.Chasing, isChasing = true ,  "Run");
                }
                break;
            case eState.Chasing:
                {
                    isIdleing = false;
                    if (this.target != null)
                    {
                        this.agent.SetDestination(this.target.position);

                        if (CalculateDistance())
                        {
                            this.agent.isStopped = true;
                            this.PlayeAnimation(eState.Attacking, isAttacking = true, "Attack01");
                        }
                    }
                }
                break;
            case eState.Angry:
                {
           
                    this.isReadying = false;
                    this.agent.speed = 4;
                    
                    this.agent.isStopped = false;
                    this.agent.SetDestination(this.target.position);

                    if (CalculateMaxDistance() < Mathf.Pow(3.5f, 2))
                    {
                        this.agent.isStopped = true;
                        this.ani.speed = 1;
                        this.PlayeAnimation(eState.Attacking, isAttacking = true, "Attack01");
                    }

                }
                break;
            case eState.Ready:
                {

                }
                break;
            case eState.Attacking:
                {
                    isChasing = false;
                    if(!isAttacking)
                    {
                        this.PlayeAnimation(eState.Ready, isReadying = true, "Shout");
                    }
                }
                break;

            case eState.Die:
                {

                }
                break;
        }
    }

    public void OnShoutFinished()
    {
       this.PlayeAnimation(eState.Angry, isAngry = true, "Run");
    }

   public void OnAttack01_Finished()
    {
        if (CalculateDistance())
        {
            Debug.Log("플레이어가 데미지를 받았다");
        }
        else
        {
            this.ani.SetTrigger("Attack01_01");
        }
    }

    public void OnAttack01_01_Finished()
    {
        if (CalculateDistance())
        {
            Debug.Log("플레이어가 데미지를 받았다");
        }
        isAttacking = false;
    }

    void PlayeAnimation(eState state ,bool a, string name = null)
    {
        this.currentState = state;

        if(name != null)
        {
            this.ani.SetTrigger(name.ToString());
        }

    }

    bool CalculateDistance()
    {
        float toTargetDistance = (this.target.transform.position - this.transform.position).sqrMagnitude;

        return toTargetDistance < this.attackDistance;
    }

    float CalculateMaxDistance()
    {
        this.distance = (this.target.transform.position - this.transform.position).sqrMagnitude;
        return distance;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
