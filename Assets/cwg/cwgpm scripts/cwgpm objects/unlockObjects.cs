using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockObjects : MonoBehaviour
{
    public BoolValue priorEvent;
    private bool priorSuccess;
    public GameObject unlockMe;
    public GameObject hideMe;

    //maybe have the active gameobjects as like, the child of one empty object thats like 'unlockable ' so you can turn it on and off easy 


    // Start is called before the first frame update
    private void Start()
    {
        priorSuccess = priorEvent.RuntimeValue;
        if (!priorSuccess)
        {
            Debug.Log("not unlocked");
            unlockMe.SetActive(false);
            return;
        }
        else
        {
            //do thing here
            unlockMe.SetActive(true);
            hideMe.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
