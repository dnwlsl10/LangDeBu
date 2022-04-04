using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    // 구체가 적을 찾아서 공격한다
    // 적이 없으면 구체가 3초 뒤에 사라진다.
    // 구체가 적을 떼리면 hp가 깍인다
    // 적이 파괴되면 구체는 적을 통과한다.
    // 적이 없을 때는 앞방향으로 나간다.


    IEnumerator Start()
    {
        yield return StartCoroutine("IEMoveUp");
        StartCoroutine("IEMoveAttack");
        yield return 0;
    }

    IEnumerator IEMoveUp()
    {
        //if(name.Contains("1"))

        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            transform.position += Vector3.up * 1 * Time.deltaTime;
            yield return 0;
        }
    }

    IEnumerator IEMoveAttack()
    {
        // 태어날 때 속력을 정하고 싶다.
        //int rVelocity = Random.Range(0, 10);
        //if (rVelocity < 3)

        for (float t = 0; t <= 2.5f; t += Time.deltaTime)
        {

            GameObject target = GameObject.FindWithTag("Barbarian");
            if (target != null)
            {
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                transform.position += dir * 2 * Time.deltaTime;
                yield return 0;
            }
            else
            {

                transform.position += Vector3.forward * 3 * Time.deltaTime;
                yield return 0;

                Destroy(gameObject, 1);
                //print("적 사라짐");
            }
        }
    }
    public float rotSpeed = 0.5f;
    float currenTime;
    void Update()
    {
        currenTime += 360 * Time.deltaTime;
        transform.Rotate(Vector3.up, rotSpeed * 360 * Time.deltaTime);
    }



    public GameObject effectPrefabs;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.name.Contains("Barbarian"))
            {
                // 이펙트를 생성한다.
                //GameObject effect = Instantiate(effectPrefabs);
                //effect.transform.position = other.gameObject.transform.position;
                //effect.transform.forward = other.gameObject.transform.forward;


                EnemyHP ehp = other.gameObject.GetComponent<EnemyHP>();
                ehp.HP--;
                print("적이 데미지를 받았다");
                if (ehp.HP <= 0)
                {
                    Destroy(other.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}

