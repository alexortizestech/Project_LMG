using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public float scuttleSpeed;
    public float turnSpeed;
    public Enemy enemy;
    public Vector3 Direction;
    public float Health;
    public float limitL, limitR;
    // Start is called before the first frame update
    void Start()
    {
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
          
            Direction = Vector3.left;
            

            
        }
        else if (transform.position.x <= limitL)
        {
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


    void Die()
    {
       
        
            Destroy(this.gameObject);
        
    }

 
}