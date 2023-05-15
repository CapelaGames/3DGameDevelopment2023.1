using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBPlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jump = 5f;
    public Camera camera;
    public float groundCheckRadius;
    public float groundCheckDistance;
    
    private Rigidbody _rigidbody;
    private Vector3 _input;
    private bool _isGrounded;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0 ,Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            //New Vector3 (0,200,0)
            _rigidbody.AddForce(Vector3.up * jump, ForceMode.VelocityChange);
        }

        _input = camera.transform.TransformDirection(_input);
        _input.y = 0f;
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();

        //_rigidbody.MovePosition(_rigidbody.position + _input * (speed * Time.deltaTime));
        if (_input.magnitude > 1)
        {
            _input.Normalize();
        }
        Vector3 movement = _input * (speed * Time.deltaTime);
        _rigidbody.AddForce(movement, ForceMode.VelocityChange); //with drag of 10
        //_rigidbody.velocity = movement;
    }

    /*void CustomGravity()
    {
        _rigidbody.AddForce(Vector3.down * 10 , ForceMode.Acceleration);
    }*/

    private bool CheckGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hitInfo, groundCheckDistance))
        {
            _rigidbody.drag = 10;
            speed = 100;
            return true;
        }
        _rigidbody.drag = 0;
        speed = 10;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position,groundCheckRadius);
        Gizmos.DrawSphere(transform.position + (Vector3.down * groundCheckDistance),groundCheckRadius);
    }
}
