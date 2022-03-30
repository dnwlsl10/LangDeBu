using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform objectToFollow;
    public float followSpeed = 10f;
    public float sensitivity = 100f;
    public float clampAngle = 20f;

    private float rotX;
    private float rotY;

    public LayerMask layerMask;
    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance = 1;
    public float maxDistance = 3;
    public float finalDistance;
    public float smoothness = 10f;
    public bool isDamage;
    void Start()
    {
        this.rotX = transform.localRotation.eulerAngles.x;
        this.rotY = transform.localRotation.y;

        this.dirNormalized = this.realCamera.localPosition.normalized;
        //Debug.Log(this.dirNormalized);
        this.finalDistance = this.realCamera.localPosition.magnitude;
        //Debug.Log(this.finalDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDamage)
        {
            //Debug.Log(this.finalDistance);
            //Debug.Log(this.dirNormalized);
            this.rotX += Input.GetAxisRaw("Mouse Y") * this.sensitivity * Time.deltaTime;
            this.rotY += Input.GetAxisRaw("Mouse X") * this.sensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            //Debug.Log(rotX);
            Quaternion rot = Quaternion.Euler(-rotX, rotY, 0);
            this.transform.rotation = rot;
        }
       
    }

    private void LateUpdate()
    {
        if (!isDamage)
        {
            transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
            finalDir = this.transform.TransformPoint(dirNormalized * maxDistance);

            RaycastHit hit;
            /*        if(Physics.Raycast(this.transform.position, dirNormalized, out hit , maxDistance))
                    {

                    }*/

            if (Physics.Linecast(this.transform.position, finalDir, out hit))
            {
                this.finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }
            else
            {
                this.finalDistance = maxDistance;
            }

            // Debug.Log(this.finalDistance);
            realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        }
          
    }
}
