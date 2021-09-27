using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public GameManager gm;
    public bool isGrounded;
    public float JumpForce;
    public KeyCode Jump;
    Rigidbody rb;
    public float speed;
    public NailedRigidbody NR;
    public float mH;
    public float Health;
    void Start()
    {
        Health = 1;

        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void Update()
    {
      //  if (!NR.isHooking)
        //{
             mH = Input.GetAxis("Horizontal");

            rb.velocity = new Vector3(mH * speed, rb.velocity.y, 0);

            if (Input.GetKeyDown(Jump) && isGrounded)
            {
                rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        //}
      /*  if (mH <=0)
        {
            //mirar a la izquierda
            transform.Rotate(new Vector3(0, -180, 0),Space.Self);
        }else if (mH > 0)
        {
            //mirar a la derecha;
            transform.Rotate(new Vector3(0, 0, 0),Space.Self);
        }
      */
        Vector3 tmpPos = transform.position;
        tmpPos.x = Mathf.Clamp(tmpPos.x, -12.5f, 26.5f);
        tmpPos.y = Mathf.Clamp(tmpPos.y, -0.5f, 19.2f);
        transform.position = tmpPos;

        if (Health <= 0)
        {
            Die();
        }

    }


    void Die()
    {
        gm.GameOver();
        Destroy(this.gameObject);
    }
}
