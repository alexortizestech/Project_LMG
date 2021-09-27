using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DamageIndex : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Movement mv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Damage: " + mv.Damage;
    }
}
