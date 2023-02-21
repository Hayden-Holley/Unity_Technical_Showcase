using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigid;
    public float JumpForce = 500;
    public float groundDistance = 0.3f;
    public LayerMask whatIsGround;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical_speed", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal_speed", Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * JumpForce);
            anim.SetTrigger("jump");
        }
        if (Physics.Raycast(
            transform.position + (Vector3.up * 0.1f),
            Vector3.down,
            groundDistance,
            whatIsGround))
        {
            anim.SetBool("grounded", true);
            anim.applyRootMotion = true;
        }
        else
        {
            anim.SetBool("grounded", false);
        }
    }
}
