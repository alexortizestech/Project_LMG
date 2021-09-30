using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScreen : MonoBehaviour
{
    Vector3 PointPos;
    public NailedRigidbody NR;
    float distance;
    public GameObject Player;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        distance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerpos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
