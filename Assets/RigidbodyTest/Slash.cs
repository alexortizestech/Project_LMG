using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public bool DamageDone;
    // Start is called before the first frame update
    public float Damage;
    public float CountSlash;
    public bool isSlashing;
    public Vector3 moveDirection;
    public KeyCode Attack;
    public const float maxDashTime = 1.0f;
    public float dashDistance = 100;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 10;
    Rigidbody rb;
    public NailedRigidbody NR;

    BaseEnemy Enemy;



    private void Awake()
    {

        CountSlash = 1;
        Damage = 1;
        rb = GetComponent<Rigidbody>();
        NR = GetComponent<NailedRigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Attack) && CountSlash == 1) //Right mouse button
        {
            isSlashing = true;
            CountSlash = 0;
            
            currentDashTime = 0;

           
            

        }
        if (currentDashTime < maxDashTime)
        {

            isSlashing = true;
            moveDirection = NR.direction * dashDistance;
            currentDashTime += dashStoppingSpeed;

        }
        else
        {
            
            moveDirection = Vector3.zero;
            DamageDone = false;
            isSlashing = false;

        }


        transform.Translate(moveDirection * dashSpeed * Time.deltaTime);

    }


  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Inside");
            if (isSlashing)
            {

                Enemy = other.GetComponent<BaseEnemy>();
                if (!DamageDone)
                {
                    CountSlash += 1;
                    Damage += 1;
                    Enemy.Health -= Damage;
                    DamageDone = true;

                }

            }
        }
    }
}

