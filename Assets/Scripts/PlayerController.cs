using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    //Invoke the cs script generated from the Input Action Asset
    PlayerControls controls;
    Vector2 move;
    public float speed = 10;
    public float jump_force = 100f;
    public float max_jumps = 1;
    private bool isJumping = false;
    private float jumps_used = 0;
    private Rigidbody rb;
    private Animator anim;
    public LayerMask layerMask;

    // Awake is called before Start()
    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //context (ctx) = Lambda Expression. Meaning it is currently not set to anything yet
        controls.Player.Move.performed += ctx =>
            SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Move.performed += ctx => move =
            ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Jump.performed += ctx =>
            Jump();
            
    }

    //Physics based updated
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed *
            Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (GroundCheck() && isJumping)
        {
            jumps_used = 0;
            anim.SetBool("grounded", true);
            isJumping= false;

        }
        if (!GroundCheck())
        {
            anim.SetBool("grounded", false);
            isJumping = true;
        }
    }

    //Graphics based update
    void Update()
    {
        anim.SetFloat("vertical_speed", move.y);
        anim.SetFloat("horizontal_speed", move.x);
    }

    //For Input System commands to work, both OnEnable() and OnDisable() are needed
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }

    void Jump()
    {
        if (jumps_used != max_jumps)
        {
            Vector3 movement = new Vector3(0.0f, jump_force, 0.0f);
            rb.AddForce(movement);
            anim.SetTrigger("jump");
            jumps_used += 1;
        }
    }

    bool GroundCheck()
    {
        if (Physics.Raycast(
             transform.position,
             Vector3.down,
             0.07f,
             layerMask
             ))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {

    }
}
