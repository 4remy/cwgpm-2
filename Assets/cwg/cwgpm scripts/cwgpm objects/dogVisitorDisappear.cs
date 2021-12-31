using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogVisitorDisappear : MonoBehaviour
{
    //if the convo was completed on a previous restaurant load, the dog visitor won't show up this time

    public BoolValue requiredEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (!requiredEvent.RuntimeValue)
        {
            return;

        }
        else
        {
            Debug.Log("Required Event Completed.");
            this.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
