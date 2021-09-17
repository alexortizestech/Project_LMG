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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= LimitR)
        {
            dirRight = false;
        }

        if (transform.position.x <= LimitL)
        {
            dirRight = true;
        }

        if (Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
       Destroy(this.gameObject);
    }
    IEnumerator ExampleCoroutine()
    {
        if (transform.position.x >= LimitR)
        {
            
            dirRight = false;
            yield return new WaitForSeconds(5);
        }

        if (transform.position.x <= LimitL)
        {
            dirRight = true;
             yield return new WaitForSeconds(5);
        }
    }

}



