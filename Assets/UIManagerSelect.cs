using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManagerSelect : MonoBehaviour
{
    public GameObject selectableObject;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(selectableObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
