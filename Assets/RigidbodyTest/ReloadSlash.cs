using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSlash : MonoBehaviour
{
    public GameObject Font;
    public float Rate;
    public bool isOff;
    public float ReloadTime;
    // Start is called before the first frame update
    void Start()
    {
        ReloadTime = 0;
        Rate = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (Time.time > ReloadTime)
            {
                ReloadTime = Time.time + Rate;
                isOff = false;
            }
        

        if (!isOff)
        {
           
            Font.gameObject.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!isOff)
        {
            if (other.GetComponent<Movement>().CountSlash != 1)
            {
                other.GetComponent<Movement>().CountSlash = 1;

                isOff = true;
                Font.SetActive(false);
            }
         
        }
    }
}
