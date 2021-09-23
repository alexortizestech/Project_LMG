using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordReturn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public float speed;

    public Vector3 destiny;
  
    
    
    // Start is called before the first frame update
    void Start()
    {

        
     
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        destiny = Player.transform.position;

        transform.position =destiny*Time.deltaTime;
        destiny = Player.transform.position;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
