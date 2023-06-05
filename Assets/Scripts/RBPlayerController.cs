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

    public Vector3 offset;
    
    private Rigidbody _rigidbody;
    
    private Vector3 _input;
    private bool _isGrounded;

    private Animator _animator;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if(_rigidbody.velocity.magnitude > 0.01f)// && _isGrounded)
        //if (_input.magnitude > 0.01f)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();
        
        if (_input.magnitude > 1)
        {
            _input.Normalize();
        }
        Vector3 movement = _input * (speed * Time.deltaTime);
        _rigidbody.AddForce(movement, ForceMode.VelocityChange);

        RotateToCameraDirection();
    }

    void RotateToCameraDirection()
    {
        transform.forward = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
    }
    
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + offset ,groundCheckRadius);
        Gizmos.DrawSphere(transform.position + offset + (Vector3.down * groundCheckDistance),groundCheckRadius);
    }
    
    private bool CheckGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position + offset, groundCheckRadius, Vector3.down, out hitInfo, groundCheckDistance))
        {
            _rigidbody.drag = 10;
            speed = 100;

            if (hitInfo.transform.tag == "Moving Platform")
            {
                transform.parent = hitInfo.transform;
            }
            else
            {
                transform.parent = null;
            }
            
            return true;
        }
        else
        {
            transform.parent = null;
        }
        _rigidbody.drag = 0;
        speed = 10;
        return false;
    }


}
