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

    // Start is called before the first frame update
    void Start()
    {
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



            }
            else if (transform.position.x <= limitL)
            {
                rb.velocity = new Vector3(0, 0, 0);
                Direction = Vector3.right;

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
        var destiny = player.position - transform.position;
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(destiny* dashSpeed);
    }


}