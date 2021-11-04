using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectsToInstantiate;

    void Start()
    {
        InstantiateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateObjects()
    {
        int n = Random.Range(0, objectsToInstantiate.Length);
        Instantiate(objectsToInstantiate[n]);

        //i want it to wait random times then do it again
    }
}
