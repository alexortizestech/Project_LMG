using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NailedRigidbody : MonoBehaviour
{
    public Vector3 limitX,limitY;
    public List<GameObject> Environment = new List<GameObject>();
    public bool isHooking;
    public Transform PlayerPos;
    public Vector2 origin, direction;
    public Vector3 endPosition;
    public float length;
    public KeyCode Hook;
    public KeyCode Cancel;
    public float HookSpeed;
    Rigidbody rb;
    bool isGrappling = false;
    public float count;
    Vector3 HookDirection;
    public float limitTime;
    public LayerMask Ground, Wall;
    public Image pointer;
    public GameObject Sword,SwordReturn;
    public int Pressed;
    public Vector3 destiny;
    GameObject clone,returner;
    public KeyCode Teleport;
    public float countObject;
    // Start is called before the first frame update
    void Start()
    {
        Pressed = 0;
        Wall = LayerMask.NameToLayer("Wall");
        Ground = LayerMask.NameToLayer("Ground");
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
       // limitX=Screen.width.
        origin = new Vector2(PlayerPos.transform.position.x, PlayerPos.transform.position.y);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        endPosition = origin + (length * direction);

        if (Physics.Raycast(ray, out hit, length))
        {

            endPosition = new Vector3(hit.point.x, hit.point.y, 0);

        }

        
       

        if (Input.GetKeyDown(Hook))
        {
                 foreach (GameObject cube in Environment)
                 {
                    cube.tag = "Wall";
                 }

           if(hit.point.x>Vector3.zero.x|| hit.point.y>Vector3.zero.y)
            if (hit.collider.CompareTag("Wall")) 
            {

                
                HookDirection = (hit.point - transform.position);
                
                
               // HookAction();
                
            }

            if (Pressed == 0)
            {
                if(direction.x!=0 || direction.y != 0)
                {
                    AlternativeHook();
                }
                
                

            }

        }


        if (Pressed == 1)
        {
            countObject += 1 * Time.deltaTime;
            destiny = clone.transform.position;
            if (Input.GetKeyDown(Teleport))
            {
                foreach (GameObject cube in Environment)
                {
                    cube.tag = "Wall";
                }
                HyperDash();
            }
               
        }

        Debug.DrawLine(PlayerPos.transform.position, endPosition, Color.green, 0);


        if (Input.GetKeyDown(Cancel))
        {
            Debug.Log("Cancelling");
            CancelHook();
        }


        if (isHooking)
        {
            
            count+=1*Time.deltaTime;
            if (count >= limitTime)
            {
                isHooking = false;
                CancelHook();
            }
        }

        if (countObject >= limitTime)
        {
            CancelHook();
        }
        if(hit.point.x!=0|| hit.point.y != 0)
        {
            if (hit.transform.gameObject.layer == Ground || hit.transform.gameObject.layer == Wall)
            {
                pointer.color = Color.green;
            }
            else
            {
                pointer.color = Color.red;
            }
        }
      
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
           
            if (collision.gameObject.layer != Ground)
            {
               
                Debug.Log("Hooked");
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else if (collision.gameObject.layer==Ground)
            {
                CancelHook();
            }
        }
        if (Input.GetKeyDown(Cancel))
        {
            Debug.Log("Cancelling");
            CancelHook();

        }

       
    }

    void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

   public void CancelHook()
    {
        foreach (GameObject cube in Environment)
        {
            cube.tag = "Untagged";
        }
        UnFreeze();
        isHooking = false;
        if (clone)
        {
            returner = Instantiate(SwordReturn, clone.transform.position, clone.transform.rotation);
        }
   
        Destroy(clone);
        Pressed = 0;
    }


    void HookAction()
    {
        UnFreeze();
        count = 0;
        isHooking = true;
        rb.velocity = HookDirection.normalized * HookSpeed;
        rb.AddForce(HookDirection.normalized * HookSpeed);
    }


    void AlternativeHook()
    {
        Pressed += 1;
       // UnFreeze();
        count = 0;
        countObject = 0;
        clone= Instantiate(Sword, transform.position, transform.rotation);
        
        
        
    }

    void HyperDash()
    {
        UnFreeze();
        isHooking = true;
        count = 0;
        countObject = 0;
        //rb.velocity = destiny.normalized * HookSpeed;
        transform.position = clone.transform.position;
        //  rb.AddForce(destiny.normalized * HookSpeed);
        Destroy(clone);
        Pressed = 0;
        // transform.position = clone.transform.position;
    }

    
}
