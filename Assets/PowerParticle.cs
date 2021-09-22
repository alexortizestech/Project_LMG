using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerParticle : MonoBehaviour
{
    public ParticleSystem ps;
    public Slash slash;
  
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;

        if (slash.Damage == 1)
        {
            main.startColor = Color.green;
        }else if (slash.Damage == 2)
        {
            main.startColor = Color.yellow;
        }
        else if (slash.Damage == 3)
        {
            main.startColor = Color.red;
        }
    }
}
