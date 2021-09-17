using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailedRigidbody : MonoBehaviour
{
    
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
    // Start is called before the first frame update
    void Start()
    {
        
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


            UnFreeze();

            isHooking = true;
            HookDirection = (hit.point - transform.position);
            rb.velocity = HookDirection.normalized * HookSpeed;
            rb.AddForce(HookDirection.normalized * HookSpeed);
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
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hooked");
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
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

    void CancelHook()
    {
        
        UnFreeze();
        isHooking = false;
    }
}
