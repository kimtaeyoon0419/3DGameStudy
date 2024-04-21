using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarLookCam : MonoBehaviour
{
    public Camera MainCamera;

    private void Update()
    {
        transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.forward, MainCamera.transform.rotation * Vector3.up);
    }
}
