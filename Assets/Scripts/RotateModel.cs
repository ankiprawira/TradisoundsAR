using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public GameObject objectRotate;

    public float rotateSpeed = 50f;
    bool rotateStatus = false;
    private bool putarArahKiri = false;
    Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (rotateStatus == true)
        {
            if (putarArahKiri)
            {
                objectRotate.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            else
            {
                objectRotate.transform.Rotate(Vector3.forward * -1, rotateSpeed * Time.deltaTime);
            }
        }
    }

    public void OnPressPutarKiri() //button sebelah kiri
    {
        putarArahKiri = false; //putar arah kiri false, objek putar arah kanan
        rotateStatus = true;
    }

    public void OnReleasePutarKiri() //button sebelah kiri
    {
        putarArahKiri = false;
        rotateStatus = false;
    }

    public void OnPressPutarKanan()
    {
        putarArahKiri = true;
        rotateStatus = true;
    }

    public void OnReleasePutarKanan()
    {
        putarArahKiri = true;
        rotateStatus = false;
    }

    public void ResetRotation()
    {
        transform.rotation = startRotation;
    }
}
