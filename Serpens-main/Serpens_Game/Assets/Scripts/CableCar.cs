using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCar : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player")
        {
            col.transform.parent = transform;
        }
    }

    //If player leaves the Cable Car, player is unparented from the car (still a work in progress)
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "player") 
        {
            col.transform.parent = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
