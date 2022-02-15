using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController charcon;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float gravity = -9.81f;
    Vector3 velocity;

    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGround;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform transformCamera;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
        charcon = GetComponent<CharacterController>();
    }
 
    // Update is called once per frame
    void Update()
    {
        //Player Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        charcon.Move(move * speed * Time.deltaTime);

        //Player Gravity
        velocity.y += gravity * Time.deltaTime;
        charcon.Move(velocity*Time.deltaTime);

        //Player check Ground
        isGround = Physics.CheckSphere(checkGround.position, 0.25f, groundMask);

        //player Jump
        if(Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //player fall from height
        if(isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Player shooting
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(transformCamera.position,transformCamera.forward,out hit, 50f))
            {
                firePoint.LookAt(hit.point);
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
