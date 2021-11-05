using UnityEngine;
using System.Collections;

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
                Debug.Log(hit.collider.gameObject.name);
                //only says room1, cant get it to hit specific item so they dont all fall down when clicked
            }
        }
    }

}