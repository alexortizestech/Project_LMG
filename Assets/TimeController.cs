using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float count;
    public float limit;
    public NailedRigidbody nr;
    // public KeyCode Slower;
    public float Slowing;
    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        limit = 5*0.25f;
        count = 5;
    }

    // Update is called once per frame
    void Update()
    {
        count += 1 * Time.deltaTime;
        Slowing = Input.GetAxis("BulletTime");
        if (Slowing==1)
        {
            pressed = true;
        }


        if (pressed)
        {
            count = 0;
            count += 1 * Time.deltaTime;
            Time.timeScale = 0.25f;
            nr.direction *= 4;

        }

        if (count >= limit)
        {
            ReturnTime();
        }

       
        Debug.Log(Time.timeScale);
    }


   
    void ReturnTime()
    {
        pressed = false;
        Time.timeScale = 1;
        count = 0;
    }
}
