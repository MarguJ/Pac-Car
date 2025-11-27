using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        cam2.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            if (cam1.enabled)
                cam1.enabled = false;
            else
                cam1.enabled = true;
            if (cam2.enabled)
                cam2.enabled = false;
            else
                cam2.enabled = true;
        }
    }
}
