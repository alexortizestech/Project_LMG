using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float LimitR;
    public float LimitL;
    public float Health;
    private bool dirRight = true;
    public float speed = 2.0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
       Destroy(this.gameObject);
    }
    IEnumerator waiter()
    {

        //Rotate 90 deg
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(4);

        //Rotate 40 deg
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Wait for 2 seconds
        yield return new WaitForSecondsRealtime(2);

        //Rotate 20 deg
       

    }

}



