using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical_speed", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal_speed", Input.GetAxis("Horizontal"));
    }
}
