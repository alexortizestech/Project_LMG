using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player,SpawnPoint;
    Rigidbody rb;
    public float scuttleSpeed;
    public float turnSpeed;
    public Enemy enemy;
    public Vector3 Direction;
    public float Health;
    public float limitL, limitR,nextFire,fireRate;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0;
        fireRate = 1.5f;
        Direction = Vector3.right;
        rb = GetComponent<Rigidbody>();
        Health = enemy.Health;
        limitL = transform.position.x - enemy.Walk;
        limitR= transform.position.x + enemy.Walk;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.position.x >= limitR)
        {
            rb.velocity = new Vector3(0,0,0);
            Direction = Vector3.left;
            

            
        }
        else if (transform.position.x <= limitL)
        {
            rb.velocity = new Vector3(0, 0, 0);
            Direction = Vector3.right;
           
        }
        rb.AddForce(Direction * scuttleSpeed *Time.deltaTime);

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
        Health -= damage;
    }
    void Die()
    {
       
        
            Destroy(this.gameObject);
        
    }

   public void Attack()
    {
        //transform.LookAt(player);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

           var Shoot= Instantiate(Bullet, transform.position, transform.rotation);

            
        }
        Debug.Log("Enemy Attack");

   }

 
}