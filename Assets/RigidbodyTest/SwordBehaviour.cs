using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public NailedRigidbody nr;
    public float speed;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        nr= GameObject.FindGameObjectWithTag("Player").GetComponent<NailedRigidbody>();
        direction = new Vector3(nr.direction.x, nr.direction.y, 0);
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=direction * speed * Time.deltaTime;
    }
}
