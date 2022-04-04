using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRay : MonoBehaviour
{
    RaycastHit hitInfo;
    public float maxDistance = 3;

    // Update is called once per frame
    void Update()
    {
        //마우스 왼쪽 버튼을 누르면 광선검이 나간다.
        if (Input.GetMouseButtonDown(0))
        {

            Debug.DrawRay(transform.position, transform.up * maxDistance, Color.blue, 1);
            if (Physics.Raycast(transform.position, transform.up, out hitInfo, maxDistance))
            {
                hitInfo.transform.GetComponent<BoxCollider>();
                print("레이가 적용되었다");

                if (hitInfo.collider.gameObject.CompareTag("Barbarian"))
                {
                    EnemyHP ehp = hitInfo.transform.GetComponent<EnemyHP>();
                    ehp.HP--;
                    print("적이 데미지를 받았다");
                    if (ehp.HP <= 0)
                    {
                        Destroy(hitInfo.transform.gameObject);
                    }
                }

                //if (hitInfo.collider.gameObject.Tag =="Barbarian")
                //if (hitInfo.collider.gameObject.CompareTag("Barbarian"))
                //{

                //    EnemyHP ehp = this.gameObject.GetComponent<EnemyHP>();
                //    ehp.HP--;
                //    print("적이 칼을 맞았다");
                //    if (ehp.HP <= 0)
                //    {
                //        Destroy(this.gameObject);
                //        print("적이 칼을 맞고 사망했다");
                //    }
                //}
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {

            if (other.gameObject.name.Contains("Barbarian"))
            {

                EnemyHP ehp = other.gameObject.GetComponent<EnemyHP>();
                ehp.HP--;
                print("적이 데미지를 받았다");
                if (ehp.HP <= 0)
                {
                    Destroy(other.gameObject);


                }
                //Destroy(this.gameObject);
                ////이펙트를 생성한다.
                //GameObject effect = Instantiate(effectPrefabs);
                //effect.transform.position = other.gameObject.transform.position;
                //effect.transform.forward = other.gameObject.transform.forward;

            }
        }
    }
}
