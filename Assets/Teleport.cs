using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public PlayerJump Jump;
    public CharacterController cc;
    public GameObject Player;
    public bool isSpawned;
    public KeyCode Tp;
    public KeyCode Spawn;
    public GameObject tpObject;
    public GameObject clone;
    public float length;
    public Transform PlayerPos;
    public Vector2 origin;
    public Vector2 direction;
    public Vector3 endPosition;
    public Vector2 SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
         origin = new Vector2(PlayerPos.transform.position.x, PlayerPos.transform.position.y);
         direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        
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
                SpawnAction();
            }
         
            
        }

        if (Input.GetKeyDown(Tp))
        {
            if (isSpawned)
            {
                tpAction();

            }

        }

    }


    void SpawnAction()
    {
        
        isSpawned = true;
        clone = Instantiate(tpObject);
        clone.transform.Translate(endPosition);
        SpawnPoint = new Vector2(clone.transform.position.x, clone.transform.position.y);
       
    }

    public void tpAction()
    {
        cc.enabled = false;
        transform.position = SpawnPoint;
        cc.enabled = true;
        //Jump.enabled = false;
        isSpawned = false;
        Destroy(clone);
    }

}
