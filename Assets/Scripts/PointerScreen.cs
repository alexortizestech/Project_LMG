using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScreen : MonoBehaviour
{
    public NailedRigidbody NR;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, NR.endPosition.normalized,distance);
        
    }
}
