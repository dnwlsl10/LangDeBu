using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallAttack : MonoBehaviour
{
    public GameObject magicBallFactory;
    public GameObject targetPosition1;
    public GameObject targetPosition2;
    public GameObject targetPosition3;

    void Update()
    {
        // 사용자의 입력에 따라
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 구체공장에서 구체를 만들어서
            GameObject magicBall1 = Instantiate(magicBallFactory);
            GameObject magicBall2 = Instantiate(magicBallFactory);
            GameObject magicBall3 = Instantiate(magicBallFactory);
            // 타겟위치로 놓고싶다.
            magicBall1.transform.position = targetPosition1.transform.position;
            //print("1번");
            magicBall2.transform.position = targetPosition2.transform.position;
            //print("2번");
            magicBall3.transform.position = targetPosition3.transform.position;
            //print("3번");
        }
    }
}
