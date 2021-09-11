using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashCounter : MonoBehaviour
{
    int numberOfTrash;
    public bool trashCleaned;
    public BoolValue storedCleaned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //there is probably a better way to do this than a constant update.
    //also consider putting a sound effect on the trash script
    // Update is called once per frame
    void Update()
    {
        numberOfTrash = GameObject.FindGameObjectsWithTag("trash").Length;

        if (numberOfTrash == 0)
        {
            trashCleaned = true;
            storedCleaned.RuntimeValue = trashCleaned;
          //  Debug.Log("trashCleaned " + " is " + trashCleaned);
        }
        else
        {
            trashCleaned = false;
           // Debug.Log("trashCleaned " + " is " + trashCleaned);
        }
    }
}
