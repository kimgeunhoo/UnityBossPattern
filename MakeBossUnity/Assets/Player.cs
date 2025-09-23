using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Controls controls;
    Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 1f;

    public bool IsJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.canceled += HandleJumpCancled;
        controls.Player.Enable();
    }


    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.canceled -= HandleJumpCancled;
        controls.Player.Disable();     
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();

        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocityY);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(IsGround() && !IsJump)
        {
            IsJump = true;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

    }
    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask); // 3À» GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, 
            transform.position + (Vector3)(Vector2.down * groundCheckDistance));// ne Vector3 (0, -1 * 3 ,0)
    }
}
