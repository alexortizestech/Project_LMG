using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NailedRigidbody : MonoBehaviour
{
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
    public GameObject Sword;
    public int Pressed;
    public Vector3 destiny;
    GameObject clone;
    public KeyCode Teleport;
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

           
            if (hit.collider.CompareTag("Wall")) 
            {

                
                HookDirection = (hit.point - transform.position);
                if (Pressed == 0)
                {
                   // AlternativeHook();
                    
                }
                
                HookAction();
                
            }

               
          
        }


        if (Pressed == 1)
        {
            destiny = clone.transform.position;
            if (Input.GetKeyDown(Teleport))
                HyperDash();
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
        if(direction.x!=0 || direction.y != 0)
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
        UnFreeze();
        count = 0;
        isHooking = true;
        clone= Instantiate(Sword, transform.position, transform.rotation);
        
        
    }

    void HyperDash()
    {
        rb.AddForce(destiny.normalized * HookSpeed);
    }
}
