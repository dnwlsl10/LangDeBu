using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
    
}
