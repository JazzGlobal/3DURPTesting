using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public bool debug = false;
    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    public float jumpAmount = 20.0f;
    public float secondsToApplyJumpForce = 3f;
    public float jumpingGravity = 1f;
    public float fallingGravity = 6f;
    public Animator animator;
    Rigidbody _rb;
    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();  
        _rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if (debug)
            DebugMovementEntity();

        Move();
    }

    void DebugMovementEntity()
    {
        Debug.Log($"RigidBody2D Position: {_rb?.position.ToString() ?? "RigidBody Component is null."}");
    }

    void Move()
    {
        var inputHorizontal = Input.GetAxis("Horizontal");
        var inputVertical = Input.GetAxis("Vertical");
        _rb.velocity = new Vector3(inputHorizontal * speed, _rb.velocity.y, inputVertical * speed);
        animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));

        if (inputHorizontal > 0 && !facingRight || inputHorizontal < 0 && facingRight)
        {
            FlipCharacter();    
        }
    }

    void FlipCharacter()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        facingRight = !facingRight;
    }
    
    public void Clean()
    {
        enabled = false;
    }
}