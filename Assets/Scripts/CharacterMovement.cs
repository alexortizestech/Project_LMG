using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Teleport teleport;
    public CharacterController controller;
    public float gravity = 20.0F;
    public float jumpSpeed;
    public Vector2 maximumSpeed = new Vector2(1.0f, 1.0f);
    public Vector2 velocity = new Vector2().normalized;
    
    void Start()
    {
         controller= GetComponent<CharacterController>();

    }

    void Update()
    {
       
        velocity.x = Input.GetAxis("Horizontal") * maximumSpeed.x;
        controller.Move(velocity * Time.deltaTime);
        //transform.Translate(velocity * Time.deltaTime);
    }

}