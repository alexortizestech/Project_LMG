using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player,SpawnPoint;
    Rigidbody2D rb;
    public float scuttleSpeed;
    public float turnSpeed;
    public Enemy enemy;
    public Vector3 Direction;
    public float Health;
    public float limitL, limitR,nextFire,fireRate;
    public GameObject Bullet;
    public bool myFunctionDone;
    public GameObject Player;
    public string attack;
    public float dashSpeed;
    public VisionRange fov;
    public float inRange;
    public float StartHealth;
    public bool Failed;

    // Start is called before the first frame update
    void Start()
    {
        
        StartHealth = enemy.Health;
        dashSpeed = 100;
        fov = GetComponent<VisionRange>();
        attack = enemy.Attack;
        nextFire = 0;
        fireRate = 1.5f;
        Direction = Vector3.right;
        rb = GetComponent<Rigidbody2D>();
        Health = enemy.Health;
        limitL = transform.position.x - enemy.Walk;
        limitR= transform.position.x + enemy.Walk;
        fov.direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {

        if (!fov.canSeePlayer)
        {
            if (transform.position.x >= limitR)
            {
                rb.velocity = new Vector3(0, 0, 0);
                Direction = Vector3.left;
                fov.direction = Vector2.left;


            }
            else if (transform.position.x <= limitL)
            {
                rb.velocity = new Vector3(0, 0, 0);
                Direction = Vector3.right;
                fov.direction = Vector2.right;
            }
            rb.AddForce(Direction * scuttleSpeed * Time.deltaTime);
        }

        if (fov.canSeePlayer)
        {
           if(attack == "Shoot")
            {
                ShootAtack();
            }

            if (attack == "Dashing")
            {
                SlashingAttack();
            }
            
        }

        Vector3 tmpPos = transform.position;
        tmpPos.x = Mathf.Clamp(tmpPos.x, limitL, limitR);
        transform.position = tmpPos;
        if (Health <= 0)
        {
            Die();
        }

        if (inRange >= 1.5f)
        {
            fov.canSeePlayer = false;
            inRange = 0;
        }


      
    }


    private void LateUpdate()
    {
      //  myFunctionDone = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (myFunctionDone && collision.GetComponent<Movement>().isDashing==false)
            {
                if (Health < StartHealth)
                {
                    Failed = true;
                    myFunctionDone = false;
                }
            }
            // myFunctionDone = false;
        }
    }
    public void TakeDamage(int damage)
    {
        if (!myFunctionDone)
        {
            Health -= damage;
            myFunctionDone = true;
        }
       
    }
    public void Die()
    {

           
            Player.GetComponent<Movement>().Killed();
            Destroy(this.gameObject);
        
    }

   public void ShootAtack()
    {
        //transform.LookAt(player);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

           var Shoot= Instantiate(Bullet, transform.position, transform.rotation);
            

            
        }
        Debug.Log("Enemy Attack");

   }


   
    public void SlashingAttack()
    {
        StartCoroutine(DashingEnemy());
    }

    IEnumerator DashingEnemy()
    {
        inRange += 1 * Time.deltaTime;
        var destiny = player.position - transform.position;
        rb.velocity = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(-player.position.x, 0, 0); //opcional
        yield return new WaitForSeconds(0.1f); //
        yield return new WaitForSeconds(0.5f); //1f
        rb.AddForce(destiny * dashSpeed);
    }
}