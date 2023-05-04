using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Invoke the cs script generated from the Input Action Asset
    PlayerControls controls;
    Vector2 move;
    Vector2 rotate;
    public float movement_speed = 10;
    public float rotation_speed = 10;
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
        //controls.Player.Move.performed += ctx =>
            //SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Move.performed += ctx => move =
            ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
        controls.Player.Jump.performed += ctx =>
            Jump();
        controls.Player.Camera.performed += ctx =>
            SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Camera.performed += ctx => rotate = 
            ctx.ReadValue<Vector2>();
        controls.Player.Camera.canceled += ctx => rotate = 
            Vector2.zero;

    }

    //Physics based updated
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0.0f, move.y) * movement_speed *
            Time.deltaTime;
        Vector3 rotation = new Vector3(0.0f, rotate.x, 0.0f) * rotation_speed *
            Time.deltaTime;
        transform.Translate(movement, Space.Self);
        transform.Rotate(rotation, Space.Self);

        if (GroundCheck() && isJumping)
        {
            jumps_used = 0;
            anim.SetBool("grounded", true);
            //Debug.Log("Grounded");
            isJumping= false;

        }
        if (!GroundCheck())
        {
            anim.SetBool("grounded", false);
            //Debug.Log("Not Grounded");
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
