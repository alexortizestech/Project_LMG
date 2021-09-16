using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    public PlayerJump Jump;
    public CharacterMovement cm;
    public CharacterController cc;
    public GameObject Player;
    public bool isSpawned;
    public KeyCode Tp;
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
    public CollisionScript CS;
    
    public float hookshotSpeed = 1f;
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
        
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        endPosition = origin + (length * direction);
        if (Physics.Raycast(ray, out hit, length))
        {
            endPosition = new Vector3(hit.point.x, hit.point.y, 0);
            
        }

       

        Debug.DrawLine(PlayerPos.transform.position, endPosition, Color.green, 0);
        Debug.Log(endPosition);
          

        
        if (Input.GetKeyDown(Spawn))
        {

            if (!isSpawned)
            {
                if (Vector3.Distance(endPosition, PlayerPos.transform.position) >= 1)
                {
                    SpawnAction();
                }
                else
                {
                    CancelHook();
                }
                    
            }
         
            
        }

        if (Input.GetKeyDown(Tp))
        {
            if (isSpawned)
            {
                tpAction();

            }

        }

        if (Input.GetKeyDown(Cancel) || Input.GetKeyDown(Jump.Jump))
        {
            CancelHook();
        }
    }


    void SpawnAction()
    {
        
        isSpawned = true;
        clone = Instantiate(tpObject);
        clone.transform.position = endPosition;
        SpawnPoint = new Vector3(clone.transform.position.x, clone.transform.position.y,0);
        CS = clone.GetComponent <CollisionScript>();
;        
      
    }

    public void tpAction()
    {

        Vector3 dir = (SpawnPoint - transform.position).normalized;
       
         cc.enabled = false;


        transform.position = Vector3.MoveTowards(transform.position, SpawnPoint, hookshotSpeed);
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
        Destroy(clone);
    }


 
  

}
