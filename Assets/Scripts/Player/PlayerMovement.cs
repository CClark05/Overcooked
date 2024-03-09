using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoves
{ 
    [SerializeField] private float moveSpeed;
    private float _moveSpeed;
    [SerializeField] private float dashSpeed;
    private float _dashSpeed;
    private float dashTimer = 0.23f;
    private float _dashTimer;
    private float dashCooldown = 0.8f;
    private float dashCooldownTimer = 0;
    public bool isDashing { get; private set; }

    public Vector2 Direction => direction;

    public Action<Vector2> OnDash;
    private Vector3 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveSpeed = moveSpeed;
        _dashSpeed = dashSpeed;
        _dashTimer = dashTimer;

    }
    private void Start()
    {
        UserInput.Instance.onDashPressed += UserInput_onDashPressed;
        GetComponent<PlayerLifeCycle>().OnDeath += (Vector2 direction) =>
        {
            this.direction = Vector3.zero;
            lastUpdatedDirection = Vector3.zero;

        };
    }
    private void Update()
    {
        if (GameManager.Instance.GetState() != GameManager.States.Playing) return;
        TakeInput();
        HandleDash();

        
    }

    private void UserInput_onDashPressed(object sender, System.EventArgs e)
    {
        if (dashCooldownTimer == 0 && dashSpeed != 0)
        {
            moveSpeed = dashSpeed;
            isDashing = true;
            OnDash?.Invoke(GetCurrentDirection());   
            dashCooldownTimer = dashCooldown;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void TakeInput()
    {
        if (isDashing || GetComponent<PlayerLifeCycle>().isDead) return;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }
    private void HandleDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashTimer = _dashTimer;
                moveSpeed = _moveSpeed;
                isDashing = false;
            }
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                dashCooldownTimer = 0;
            }
        }
    }

    private Vector3 lastUpdatedDirection = Vector3.zero;


    public Vector3 GetLastUpdatedDirection()
    {
        if(direction != Vector3.zero)
        {
            lastUpdatedDirection = direction;
        }
        return lastUpdatedDirection;
    }

    public Vector3 GetCurrentDirection()
    {
        return direction;
    }
    public void FreezeInput()
    {
        moveSpeed = 0;
        dashSpeed = 0;
    }
    public void UnFreezeInput()
    {
        moveSpeed = _moveSpeed;
        dashSpeed = _dashSpeed;
    }

}
