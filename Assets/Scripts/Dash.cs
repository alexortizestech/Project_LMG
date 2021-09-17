using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isSlashing;
    public Vector3 moveDirection;
    public KeyCode Slash;
    public const float maxDashTime = 1.0f;
    public float dashDistance = 100;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 1;
    CharacterController controller;
    Teleport tp;
    CharacterMovement cm;
    BaseEnemy Enemy;



    private void Awake()
    {
        cm = GetComponent<CharacterMovement>();
        controller = GetComponent<CharacterController>();
        tp = GetComponent<Teleport>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Slash)) //Right mouse button
        {
            controller.enabled = true;
            currentDashTime = 0;
            cm.velocity.y = 0;
           

        }
        if (currentDashTime < maxDashTime)
        {
            isSlashing = true;
            moveDirection = tp.direction * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            isSlashing = false;
            moveDirection = Vector3.zero;
        }
        controller.Move(moveDirection * Time.deltaTime * dashSpeed);


        
    }


    IEnumerator ExampleCoroutine()
    {



        isSlashing = true;
        yield return new WaitForSeconds(1);
        isSlashing = false;

        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Inside");
            if (isSlashing)
            {
                Enemy = other.GetComponent<BaseEnemy>();
                Enemy.Health--;
            }
        }
    }
}
    

