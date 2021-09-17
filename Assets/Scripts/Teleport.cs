using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool UnableObject;
    public bool Nailed2;
    public PlayerJump Jump;
    public CharacterMovement cm;
    public CharacterController cc;
    public GameObject Player;
    public bool isSpawned;
    public KeyCode Slash;
    public KeyCode Spawn;
    public KeyCode Cancel;
    public GameObject tpObject;
    public GameObject clone;
    public float length;
    public Transform PlayerPos;
    public Vector2 origin;
    public Vector2 direction;
    public Vector3 endPosition;
    public Vector3 SpawnPoint;
    public Vector3 Min;
    public Vector3 SlashDir;
    public CollisionScript CS;
    public float _dashSpeed = 0.001f;
    public float _dashTime = 0.01f;
    public float hookshotSpeed = 1f;
    public float count;
    public LayerMask Wall;
    // Start is called before the first frame update
    void Start()
    {
        Min = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
         origin = new Vector2(PlayerPos.transform.position.x, PlayerPos.transform.position.y);
         direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
         SlashDir = new Vector3(direction.x, direction.y, 0);
        
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        endPosition = origin + (length * direction);

        if (Physics.Raycast(ray, out hit, length,Wall))
        {
            endPosition = new Vector3(hit.point.x, hit.point.y, 0);
            
        }

       

        Debug.DrawLine(PlayerPos.transform.position, endPosition, Color.green, 0);
      //  Debug.Log(endPosition);

        
        
        if (Input.GetKeyDown(Spawn))
        {
            if (isSpawned && !UnableObject)
            {
                tpAction();
                
                
            }
            else if (!isSpawned && !UnableObject)


            {
                if (Vector3.Distance(endPosition, PlayerPos.transform.position) >= 1)
                {
                    SpawnAction();
                }
                else
                {
                    CancelHook();
                }
                if (clone == null)
                {
                    CancelHook();
                }
            }

           
            if (UnableObject)
            {
                if (hit.transform.tag == "Wall")
                {
                    Nailed2 = true;
                }
                InstantTeleport();
                
            }
            
        }


        if (isSpawned)
        {
            if (clone == null)
            {
                CancelHook();
            }
        }
        
        if (Input.GetKeyDown(Cancel))
        {
            CancelHook();
        }
        
        if (CS.Nailed)
        {
            count += 1 * Time.deltaTime;
            if(count>3f)
            {
                CancelHook();
            }
        }
    }


    void SpawnAction()
    {
        count = 0;
        isSpawned = true;
        clone = Instantiate(tpObject);
        clone.transform.position = endPosition;
        SpawnPoint = new Vector3(clone.transform.position.x, clone.transform.position.y,0);
        CS = clone.GetComponent <CollisionScript>();
        Destroy(clone, 3f);
;        
      
    }

    public void tpAction()
    {
        count = 0;

        Vector3 dir = (SpawnPoint - transform.position).normalized;
       
         cc.enabled = false;


        transform.position += Vector3.MoveTowards(transform.position, SpawnPoint, hookshotSpeed);
       
        cm.velocity = dir;

        if (CS.Nailed)
        {
            cc.enabled = false;
            
        }
        else
        {
            cc.enabled = true;
        }
        
        //Jump.enabled = false;
        isSpawned = false;
        Destroy(clone);
        
    }


    public void CancelHook()
    {
        cc.enabled = true;
        isSpawned = false;
        if (clone)
        {
            Destroy(clone);
        }
      
        count = 0;
    }

    void InstantTeleport()
    {
        cc.enabled = false;

        
        transform.position = endPosition;
        if (Nailed2)
        {
            cc.enabled = false;
        }
        else
        {
            cc.enabled = true;
        }

    }


}
