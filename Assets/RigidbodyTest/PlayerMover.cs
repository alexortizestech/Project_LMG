using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public bool isGrounded;
    public float JumpForce;
    public KeyCode Jump;
    Rigidbody rb;
    public float speed;
    public NailedRigidbody NR;

    void Start()
    {
       

        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void Update()
    {
        if (!NR.isHooking)
        {
            float mH = Input.GetAxis("Horizontal");

            rb.velocity = new Vector3(mH * speed, rb.velocity.y, 0);

            if (Input.GetKeyDown(Jump) && isGrounded)
            {
                rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }
        Vector3 tmpPos = transform.position;
        tmpPos.x = Mathf.Clamp(tmpPos.x, -12.5f, 26.5f);
        tmpPos.y = Mathf.Clamp(tmpPos.y, -0.5f, 19.2f);
        transform.position = tmpPos;
    }
}
