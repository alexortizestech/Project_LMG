using UnityEngine;

public class SwordReturn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public float speed;
    public GameObject This;
    public Vector3 destiny;



    // Start is called before the first frame update
    void Start()
    {

        This = this.gameObject;

        speed = 100;
    }

    // Update is called once per frame
    void Update()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        SetParent();
        // destiny = Player.transform.position;

        transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Player.transform.position, speed * Time.deltaTime);
        if (this.gameObject.transform.position == Player.transform.position)
        {
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }


    public void SetParent()
    {
        This.transform.parent = Player.transform;
    }
}
