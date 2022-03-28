using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeerMove : MonoBehaviour
{
    Transform pivot;


    public void Start()
    {
        this.pivot = this.gameObject.GetComponentInParent<Transform>();
    }
    public void Attack()
    {

        this.pivot.rotation *= this.pivot.rotation * Quaternion.Euler(90, 0, 0);
    }
}
