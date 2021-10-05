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
    public float pressed;
    public Movement mv;
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
       

        count += 1 * Time.deltaTime;


        if (mv.player.GetButtonUp("BulletTime"))
        {
            pressed++;
            
            if (pressed%2!=0 && mv.canBulletTime)
            {
                count = 0;
                // count += 1 * Time.deltaTime;
                Time.timeScale = 0.25f;
                nr.direction *= 4;
            }

            if (pressed % 2 == 0 && mv.canBulletTime)
            {
                ReturnTime();
            }

        }

        if (count >= limit)
        {
            ReturnTime();
        }

        if (Input.GetKeyDown(nr.Cancel))
        {
            ReturnTime();
        }
        Debug.Log(Time.timeScale);
    }


   
    void ReturnTime()
    {
        //pressed = false;
        Time.timeScale = 1;
        //count = 0;
    }
}
