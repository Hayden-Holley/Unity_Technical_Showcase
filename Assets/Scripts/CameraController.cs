using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    PlayerControls controls;
    public GameObject target;
    Vector2 move;
    public float camera_speed = 10;

    //Awake is called before Start()
    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Camera.performed += ctx => 
            move = ctx.ReadValue<Vector2>();
        //controls.Player.Camera.performed += ctx => SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Camera.canceled += ctx => 
            move = Vector2.zero;
    }

    //Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 axis = new Vector3(target.transform.position.y, 0.0f, 0.0f);
        transform.RotateAround(
            target.transform.position, 
            target.transform.right, 
            move.y * camera_speed * Time.deltaTime);
        //transform.Rotate(camera_movement, Space.Self);
    }

    //Update is called once per frame
    void Update()
    {

    }

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
}
