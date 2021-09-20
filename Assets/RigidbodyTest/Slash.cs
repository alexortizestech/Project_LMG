using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{   public Transform attackpoint;
    public Vector3 attackRange;
    public LayerMask EnemyLayer;
    public bool DamageDone;
    // Start is called before the first frame update
    public int Damage;
    public float CountSlash;
    public bool isSlashing;
    public Vector3 moveDirection;
    public KeyCode Attack;
    public const float maxDashTime = 1.0f;
    public float dashDistance=25f;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 7.5f;
    Rigidbody rb;
    public NailedRigidbody NR;
    public float Range;

    EnemyBehaviour Enemy;



    private void Awake()
    {
        Range = 5;
        attackRange = new Vector3(4, 2, 0);
        CountSlash = 1;
        Damage = 1;
        rb = GetComponent<Rigidbody>();
        NR = GetComponent<NailedRigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Attack) && CountSlash == 1) 
        {
            isSlashing = true;
            CountSlash = 0;
            
            currentDashTime = 0;

           AttackSlash();



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
        if (CountSlash >= 1)
        {
            CountSlash = 1;
        }
        OnDrawGizmos();
    }

    
  
 /*  private void OnTriggerEnter(Collider other)
    {
        DamageDone = false;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Inside");
            if (isSlashing)
            {

                Enemy = other.GetComponent<EnemyBehaviour>();
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
 */
   void AttackSlash()
    {
       // Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Range,  EnemyLayer);
        Collider[] hitEnemies = Physics.OverlapBox(transform.position, attackRange, Quaternion.Euler(NR.direction.x,NR.direction.y,0), EnemyLayer);


        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(Damage);
            CountSlash = 1;
            Damage += 1;
        }
    }
   void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, attackRange);
       // Gizmos.DrawSphere(transform.position, Range);
    }

   /* private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().Health -= Damage;
            Debug.Log("Inside");
        }
            
    }*/
}

