using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class clickManager : MonoBehaviour
{
    public LayerMask IgnoreMe;

    void Start ()
    {
        IgnoreMe = LayerMask.NameToLayer("MainCamera");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

             RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero,  ~IgnoreMe);
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Gem>())
                {
                    Debug.Log("Gem clicked");
                    hit.collider.gameObject.SetActive (false);
                    Timer.timeRemaining += 20;
                    return;
                }
                Debug.Log(hit.collider.gameObject.name);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                //rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
hit.collider.attachedRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

}