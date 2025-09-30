using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    public Controls controls;
    Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 1f;

    [SerializeField] private int MaxHealth = 100;

    AudioSource audioSource;

    [field: SerializeField] public int CurrentHealth { get; private set; }

    public bool IsJump;

    public Action<bool> OnFire;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.canceled += HandleJumpCancled;
        controls.Player.Fire.performed += OnFirePerformed;
        controls.Player.Fire.canceled += OnFireCanceled;

        controls.Player.Enable();
    }


    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.canceled -= HandleJumpCancled;
        controls.Player.Fire.performed -= OnFirePerformed;
        controls.Player.Fire.canceled -= OnFireCanceled;
        controls.Player.Disable();     
    }
    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(true);
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(false);
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocityY);

        // audiosource  소스가 끝나고 난 후에 실행하도록 만드는 코드
        audioSource.clip = Resources.Load<AudioClip>("Sound/Player_Step_wood");
        audioSource.Play();
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(IsGround() && !IsJump)
        {
            IsJump = true;
            audioSource.clip = Resources.Load<AudioClip>("Sound/Jump");
            audioSource.Play();
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

    }
    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask); // 3을 GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, 
            transform.position + (Vector3)(Vector2.down * groundCheckDistance));// ne Vector3 (0, -1 * 3 ,0)
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
