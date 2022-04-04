using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float jumpPower = 6;
    public float gravity = -9.8f;
    float yVelocity;
    public float speed = 5f;
    int jumpCount;
    public int maxJumpCount = 2;

    CharacterController cc;

    public GameObject UIinventory;
   
    void Start()
    {
        this.cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        // 사용자가 z키를 누르면 "구르기"라고 출력된다.
        if (Input.GetKeyDown("z"))
        {
            //print("구르기");
        }
        // 사용자가 마우스 왼쪽 키를 누르면 "공격"이라고 출력된다.
        if (Input.GetMouseButtonDown(0))
        {
           // Debug.Log("공격");
        }

        if (cc.isGrounded)
        {
            jumpCount = 0;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }


        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (this.UIinventory.gameObject.activeSelf)
            {
                UIinventory.SetActive(false);
            }
            else
            {
                UIinventory.SetActive(true);
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);


        transform.position = transform.position + dir * speed * Time.deltaTime;

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        Vector3 velocity = dir * speed;

        velocity.y = yVelocity;

        cc.Move(velocity * Time.deltaTime);


        //좌우 회전
        float q = Input.GetAxis("QE");
        transform.Rotate(Vector3.up, q * 0.3f * 360 * Time.deltaTime);


    }


    public bool isDamageing;

    public void OnDamage()
    {
        this.isDamageing = true;
    }


}
