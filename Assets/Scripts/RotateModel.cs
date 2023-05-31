using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public GameObject objectRotate;

    public float rotateSpeed = 50f;
    bool rotateStatus = false;
    private bool putarKiri = false;
    Quaternion startRotation;

    // //rotate object function
    // public void RotateObject(bool putarKiri)
    // {
    //     this.putarKiri=putarKiri;
    //     if (rotateStatus == false)
    //     {
    //         rotateStatus = true;
    //     }
    //     else
    //     {
    //         rotateStatus = false;
    //     }
    // }

    // public void StopRotation(){
    //     rotateStatus = false;
    // }

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (rotateStatus == true)
        {
            if (putarKiri)
            {
                objectRotate.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            else
            {
                objectRotate.transform.Rotate(Vector3.forward * -1, rotateSpeed * Time.deltaTime);
            }
        }
    }

    // IEnumerator Wait(){
    //     yield return new WaitForSecondsRealtime(1);
    //     startScale = transform.localRotation;
    // }

    public void OnPressPutarKiri() //button sebelah kiri
    {
        putarKiri = false; //putar arah kiri false, objek putar arah kanan
        rotateStatus = true;
    }

    public void OnReleasePutarKiri() //button sebelah kiri
    {
        putarKiri = false;
        rotateStatus = false;
    }

    public void OnPressPutarKanan()
    {
        putarKiri = true;
        rotateStatus = true;
    }

    public void OnReleasePutarKanan()
    {
        putarKiri = true;
        rotateStatus = false;
    }

    public void ResetRotation()
    {
        transform.rotation = startRotation;
    }
}
