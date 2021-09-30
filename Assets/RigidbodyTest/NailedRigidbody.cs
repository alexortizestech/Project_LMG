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
    Rigidbody2D rb;
    bool isGrappling = false;
    public float count;
    Vector3 HookDirection;
    public float limitTime;
    public LayerMask Ground, Wall;
    public Image pointer;
    public GameObject AimSprite;
    public GameObject Sword,SwordReturn;
    public int Pressed;
    public Vector3 destiny;
    GameObject clone,returner;
    public KeyCode Teleport;
    public float countObject;
    public bool CollidingSword;
    public Collision coll;
    public Movement mv;
    public Transform HookSpawnPoint;
    public GameObject LeftSpawn, RightSpawn, DownSpawn;
    // Start is called before the first frame update
    void Start()
    {
        mv = GetComponent<Movement>();
        coll = GetComponent<Collision>();
        Pressed = 0;
        Wall = LayerMask.NameToLayer("Wall");
        Ground = LayerMask.NameToLayer("Ground");
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
       // limitX=Screen.width.
        origin = new Vector2(PlayerPos.transform.position.x, PlayerPos.transform.position.y);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
      

        if(direction.x !=0 ||direction.y != 0)
        {
            AimSprite.SetActive(true);
        }else if(direction.x == 0 && direction.y == 0)
        {
            AimSprite.SetActive(false);
        }

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
            //if (hit.collider.CompareTag("Wall")) 
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

        if (!isHooking)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        }


        if (coll.onGround)
        {
            coll.onWall = false;
            coll.onRightWall = false;
            coll.onLeftWall = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Wall"))
        {
            isHooking = true;

            if (collision.gameObject.layer != Ground)
            if (collision.gameObject.layer != Ground)
            {
                isHooking = true;
              
                Debug.Log("Hooked");
               // rb.constraints = RigidbodyConstraints2D.FreezeAll;
               // rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else if (collision.gameObject.layer==Ground)
            {
                if (isHooking) {
                   // CancelHook();
                }
                
            }
        }
        if (Input.GetKeyDown(Cancel))
        {
            Debug.Log("Cancelling");
            CancelHook();

        }

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //count += 1 * Time.deltaTime;
        if (count >= limitTime)
        {
            CancelHook();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
       // isHooking = false;
       // UnFreeze();
    }

    void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

   public void CancelHook()
    {

        if (coll.onCeiling == true)
        {
            rb.AddForce(Vector3.down * Time.deltaTime);
        }
        mv.wallGrab = false;
        mv.wallSlide = false;
        mv.canMove = true;
        coll.onWall = false;
        coll.onCeiling = false;
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
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

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
        if (coll.onLeftWall)
        {
            HookSpawnPoint = RightSpawn.transform;
        }else if (coll.onRightWall)
        {
            HookSpawnPoint = LeftSpawn.transform;
        }else if (coll.onCeiling)
        {
            HookSpawnPoint = DownSpawn.transform;
        }
        else
        {
            HookSpawnPoint = transform;
        }


        clone= Instantiate(Sword, HookSpawnPoint.position, transform.rotation);
        
        
        
    }

    void HyperDash()
    {


        UnFreeze();
        isHooking = true;
        count = 0;
        countObject = 0;
        //rb.velocity = destiny.normalized * HookSpeed;
        transform.position = clone.transform.position;
        if (CollidingSword)
        {
           // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
       // isHooking = false;
        //  rb.AddForce(destiny.normalized * HookSpeed);
        Destroy(clone);
        Pressed = 0;
        // transform.position = clone.transform.position;
    }

    
}
