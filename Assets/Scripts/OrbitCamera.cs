using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float rotationSpeed = 90f;
    private Vector2 _orbitAngles = new Vector2(45f, 0);

    public Transform _focus;

    /*private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }*/
    //Its exactly like Update, expect it runs late, which is useful for camera code.
    private void LateUpdate()
    {
        Quaternion lookRotation = transform.localRotation;
        
        if (ManualRotation())
        {
            lookRotation = Quaternion.Euler(_orbitAngles);
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = _focus.position - lookDirection * 5f;
        
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    private bool ManualRotation()
    {
        Vector2 input = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        float deadzone = 0.001f;
        if (input.x < -deadzone || input.x > deadzone || input.y < -deadzone || input.y > deadzone)
        {
            _orbitAngles +=  input * rotationSpeed * Time.unscaledDeltaTime;
            return true;
        }

        return false;
    }
}
