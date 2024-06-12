using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 9f;
    public Animator animator;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private float directionX;
    private float directionY;
    private float lastDashTime = Time.time;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void OnEnable(){
        playerControls.Enable();
    }

    private void Update(){

        if (Time.time > lastDashTime + .075f) {
            PlayerInput();
            lastDashTime = Time.time;
        }

    }
    private void FixedUpdate(){
        Move();
    }

    private void PlayerInput(){
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
        if (movement.x != 0 && movement.y != 0)
        {
            directionX = movement.x;
            directionY = movement.y;
        }
        else if (movement.x != 0 || movement.y != 0)
        {
            directionX = movement.x;
            directionY = movement.y;
        }
        animator.SetFloat("DirectionX", directionX);
        animator.SetFloat("DirectionY", directionY);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void Move(){
        rb.MovePosition(rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }

}
