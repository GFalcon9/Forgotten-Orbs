using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    public bool canMove = true;

    float speed;
    public float walkSpeed = 1.5f;
    public float runSpeed = 4f;

    bool isWalking;
    bool isRunning;

    bool isJumping;
    public float jumpHeight;
    Vector3 jumpDistance;

    private Camera mainCamera;

    bool isMoving;

    private Quaternion freeRotation;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(isMoving)
        {
            if (isWalking)
            {
                speed = walkSpeed;
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsRunning", false);
            }

            if (isRunning)
            {
                speed = runSpeed;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", true);
            }
        }

        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }

    }

    void FixedUpdate()
    {
        Vector3 moveDirection;
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        if (canMove)
        {                                                                                                                       
            Vector3 movement = new Vector3(moveDirection.y, 0.0f, moveDirection.x);                                                    
                                                                                                                                   
            if (movement != Vector3.zero)                                                                                          
            {                                                                                                                      
                isMoving = true;

                //Rotation......
                var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;
                var right = mainCamera.transform.TransformDirection(Vector3.right);
                movement = moveDirection.x * right + moveDirection.y * forward;

                Vector3 lookDirection = movement.normalized;

                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), 0.15F);
            }
            else
            {
                isMoving = false;
            }
                                                                                                                                   
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                isWalking = false;
            }
            else
            {
                isWalking = true;
                isRunning = false;
            }

            jumpDistance = new Vector3(0, jump, 0);
            transform.Translate(jumpDistance * Time.deltaTime * jumpHeight);

            if (jump > 0.1f)
            {
                isJumping = true;
                anim.SetBool("IsJumping", true);
            }
            else
            {
                isJumping = false;
                anim.SetBool("IsJumping", false);
            }
        }
        else
        {
            isMoving = false;
        }
    }
}
