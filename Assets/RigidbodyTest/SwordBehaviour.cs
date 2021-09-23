using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public NailedRigidbody nr;
    public float speed;
    Vector3 direction;
    public Vector3 lastPos;
    public LayerMask Wall;
    public LayerMask Ground;
    Rigidbody rb;
    bool Colliding;
    // Start is called before the first frame update
    void Start()
    {
      //  rb.GetComponent<Rigidbody>();
        Wall = LayerMask.NameToLayer("Wall");
        Ground = LayerMask.NameToLayer("Ground");
        nr = GameObject.FindGameObjectWithTag("Player").GetComponent<NailedRigidbody>();
        direction = new Vector3(nr.direction.x, nr.direction.y, 0);
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Colliding)
        {
            transform.position +=direction * speed * Time.deltaTime;
        }

       
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Wall || other.gameObject.layer == Ground)
        {
            //transform.position = transform.position;
            Colliding = true;
;            Debug.Log("collision");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Wall || collision.gameObject.layer == Ground)
        {
            Debug.Log("collision");
           // rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
