using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<itemCollisions>().LandedEvent += onLandedEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void onLandedEvent()
    {
        FindObjectOfType<itemCollisions>().LandedEvent -= onLandedEvent;
       //code goes on the pizza
    }
    
}
