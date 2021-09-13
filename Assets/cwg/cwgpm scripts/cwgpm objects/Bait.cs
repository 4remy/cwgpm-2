using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SchwerInventory = Schwer.ItemSystem.Inventory;
using UnityEngine.UI;
using SchwerInventory = Schwer.ItemSystem.Inventory;


public class Bait : MonoBehaviour
{
    [Header("Animation")]
    private Animator animator;

    public Signal BaitSignal;
    public bool baitZone;

    private bool trapped;
    private bool grabbingOut;

    public GameObject bait;
    public GameObject cutHandFake;
    public GameObject cutHandReal;

    private bool thatsAll;

    //public string killSoundEffect;

    void Awake()
    {

        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        bait.SetActive(false);
        cutHandFake.SetActive(false);
        cutHandReal.SetActive(false);
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
                StartCoroutine(killCo());
            }

        }
    }

    public void ChildTrigger()
    {
        if(!grabbingOut)
        {
            Debug.Log("general zone triggered");
            animator.SetBool("flee", true);
        }
        else
        {
            animator.SetBool("grabOut", true);
        }
    }

    public void ChildTriggerExit()
    {
        Debug.Log("general zone exit");
        animator.SetBool("flee", false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "baitChild")
        {
            return; // do nothing
        }
        else
        {
            //Debug.Log("player in bait zone");
            baitZone = true;
           // Debug.Log("baitZone " + " is " + baitZone);
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
           // Debug.Log("player has exited bait zone");
            baitZone = false;
           // Debug.Log("baitZone" + " is " + baitZone);
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
       // Debug.Log("baitZone " + " is genuinely " + baitZone);
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
        if(!thatsAll)
        {
            Debug.Log("baited coroutine");
            animator.SetBool("flee", false);
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
            grabbingOut = true;
            animator.SetBool("grabOut", true);
            yield return new WaitForSeconds(4f);
            grabbingOut = false;
            animator.SetBool("grabOut", false);
        }
        else
        {
            bait.SetActive(true);
        }
    }

    IEnumerator killCo()
    {
        bait.SetActive(false);
        // AudioManager.Instance.Play(killSoundEffect);
        animator.SetBool("cut", true);
        yield return new WaitForSeconds(0.1f);
        cutHandFake.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cutHandFake.SetActive(false);
        cutHandReal.SetActive(true);
        yield return new WaitForSeconds(3f);
        //bool saying event is over
        thatsAll = true;
    }
}