using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SchwerInventory = Schwer.ItemSystem.Inventory;
using UnityEngine.UI;

public class Bait : MonoBehaviour
{
    // [SerializeField] private Schwer.ItemSystem.Item item = default;


    //i think interact isnt working because of the triggers

    [Header("Animation")]
    private Animator animator;

    public Signal BaitSignal;
    public bool baitZone;

    private bool trapped;

    public GameObject bait;



    void Awake()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        bait.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && baitZone)
        {
           if(!trapped)
            {
                Debug.Log("you have interacted lol");
            }
            else
            {
                Debug.Log("now is the Time");
            }

        }
    }

    public void ChildTrigger()
    {
        Debug.Log("general zone triggered");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "baitChild")
        {
            return; // do nothing
        }
        else
        {
            Debug.Log("player in bait zone");
            baitZone = true;
            Debug.Log("baitZone " + " is " + baitZone);
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
            Debug.Log("player has exited bait zone");
            baitZone = false;
            Debug.Log("baitZone" + " is " + baitZone);
    }



    public void Use()
    {
        //it never registers as in range
        BaitSignal.Raise();
        Debug.Log("signal generated");
        //doesnt say anything if no signal received.
    }

    public void baitRecieved()
    {
        Debug.Log("code can recieve signal");
        Debug.Log("baitZone " + " is genuinely " + baitZone);
        if (!baitZone)
        {
            Debug.Log("signal recieved");
            Debug.Log("not in range tho");
        }
        else
        {
            Debug.Log("signal recieved");
            Debug.Log("Dana voice: lets goo");
            StartCoroutine(baitedCo());
        }

    }

  IEnumerator baitedCo()
    {
        Debug.Log("baited coroutine");
        bait.SetActive(true);
        animator.SetBool("return", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("return", false);
        animator.SetBool("grab", true);
        trapped = true;

        yield return new WaitForSeconds(5f);
        trapped = false;
        bait.SetActive(false);
        animator.SetBool("grab", false);
        animator.SetBool("flee", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("flee", false);
        //go to 'flee idle'
    }
}