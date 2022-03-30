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
    Shout,
    Transcriber,
    RangeAttack,
    FallBack,
    Die
}
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private eState currentState;
    private Animator ani;
    public System.Action OnEnalbe;
    public CameraMove cameraMove;
    public Transform transcriberTrans;
    float distance;
    float maxDistance = Mathf.Pow(5,2);
    float attackDistance = Mathf.Pow(2.5f, 2);
    public Player player;

    float shoutCount = 1;
    public bool isAttacking = false;
    public bool isReadying = false;
    public bool isChasing = false;
    public bool isIdleing = false;
    public bool isAngry = false;
    public bool isShout = false;
    public bool isRotating = false;


    //확률
    public int rand;  
    //플레이어 데미지 
    private float damage;

    [System.Obsolete]
    void Start()
    {
        this.rand = Random.RandomRange(0, 4);
        this.player= GameObject.FindObjectOfType<Player>();
        this.transcriberTrans = player.onHiDagem;
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


        var dir1 = this.target.transform.position - this.transform.position;
        dir1.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir1), Time.deltaTime * 10f);

    }

    void ChangeStae(eState state)
    {
        switch (state)
        {
            case eState.Idle:
                {
                    if (isAngry)
                    {
                        this.agent.speed = 4;
                        this.ani.SetTrigger("Run");
                        this.currentState = eState.Angry;
                    }
                    else
                    {
                        this.agent.isStopped = false;
                        this.ani.SetTrigger("Run");
                        this.currentState = eState.Chasing;
                    }     
                }
                break;
            case eState.Chasing:
                {
                    if (this.target != null)
                    {
                        this.agent.SetDestination(this.target.position);

                        if (CalculateDistance())
                        {
                            this.agent.isStopped = true;

                            this.ani.SetTrigger("Attack01");
                            this.currentState = eState.Attacking;
                            this.isAttacking = true;
                            this.isRotating = true;
                        }
                    }
                }
                break;
            case eState.Angry:
                {
                    this.agent.isStopped = false;
                    this.agent.SetDestination(this.target.position);

                    if (CalculateMaxDistance() < Mathf.Pow(2.5f, 2))
                    {
                        this.agent.isStopped = true;
                        this.ani.speed = 1;
                        this.ani.SetTrigger("Attack01");
                        this.currentState = eState.Attacking;
                        this.isAttacking = true;
                        this.isRotating = true;
                    }
                }
                break;
            case eState.Attacking:
                {
                    if(!isAttacking)
                    {
                        this.currentState = eState.Shout;
                        this.ani.SetTrigger("Shout");
                        this.isShout = true;
                    }
                }
                break;
            case eState.Ready:
                {
                    var dir = this.target.position - this.transform.position;
                    this.transform.position += dir.normalized * 5f * Time.deltaTime;
                    if(CalculateMaxDistance() < Mathf.Pow(4, 2))
                    {
                        //this.gameObject.SetActive(false);
                        this.currentState = eState.Transcriber;
                    }

                }
                break;
            case eState.Shout:
                {
                    if (!this.isShout)
                    {
                        shoutCount++;
                        
                        if(shoutCount == 2)
                        {
                            this.ani.SetTrigger("Ready");
                            this.currentState = eState.Ready;
                            isReadying = true;
                        }
                        else
                        {
                            this.currentState = eState.Idle;
                            isAngry = true;
                        }
                    }
                }
                break;
            case eState.Transcriber:
                {
                   
                    this.gameObject.SetActive(true);
                    this.transform.position = transcriberTrans.position;
                    this.cameraMove = GameObject.FindObjectOfType<CameraMove>();
                    this.cameraMove.isDamage = true;
                    var dir = this.cameraMove.transform.position - this.transform.position;
                    Camera.main.fieldOfView = 80f;
                    this.cameraMove.transform.rotation =  Quaternion.LookRotation(transcriberTrans.position);
                    this.agent.isStopped = true;
                    this.ani.SetTrigger("Transcriber");
                    this.currentState = eState.Attacking;
                    this.isAttacking = true;
                   
                }
                break;
            case eState.RangeAttack:
                {
                    this.ani.SetTrigger("RangeAttack");
                    Debug.Log("test");
                    //this.currentState = eState.FallBack;
                }
                break;
            case eState.FallBack:
                {
                 
                }
                break;
            case eState.Die:
                {

                }
                break;
        }
    }

    public void OnTranscriber()
    {
        var dir = this.target.transform.position - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1f);
        if (CalculateDistance())
        {
            Debug.Log("플레이어가 데미지를 받았다");
            this.cameraMove.isDamage = false;
            Camera.main.fieldOfView = 80f;
            this.currentState = eState.Angry;
        }
        else
        {
            Debug.Log("두번째 공격을 한다.");
            this.ani.SetTrigger("Attack01_01");
        }
    }



    public void OnShoutFinished()
    {
        isShout = false;
    }

   public void OnAttack01_Finished()
    {
        var dir = this.target.transform.position - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1f);
        if (CalculateDistance())
        {
            Debug.Log("플레이어가 데미지를 받았다");
        }
        else
        {
            Debug.Log("두번째 공격을 한다.");
            this.ani.SetTrigger("Attack01_01");
        }
    }

    public void OnAttack01_01_Finished()
    {
        var dir = this.target.transform.position - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1f);
        if (CalculateDistance())
        {   
            Debug.Log("플레이어가 데미지를 받았다");
        }
       
    }

    public void OnAttackFinished()
    {
        isAttacking = false;
        this.currentState = eState.RangeAttack;
        this.ani.SetTrigger("RangeAttack");
        Debug.Log("test");
        if (this.rand > 3)
        {
           
        }
        else
        {
            this.currentState = eState.FallBack;
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
