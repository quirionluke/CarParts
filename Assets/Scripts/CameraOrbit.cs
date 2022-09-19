using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraOrbit : MonoBehaviour
{
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                return 0;
            }
        }
        else if (axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;


                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }
}
