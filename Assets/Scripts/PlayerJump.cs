using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform feetPos;
    public bool isGrounded;
    public float distanceToGround;
  
    
    public CharacterMovement cm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(feetPos.transform.position, Vector3.down);

        isGrounded = Physics.Raycast(ray, distanceToGround, LayerMask.GetMask("Ground"));
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            cm.velocity.y = cm.jumpSpeed;
        }
       cm.velocity.y -= cm.gravity * Time.deltaTime;
    }
}
