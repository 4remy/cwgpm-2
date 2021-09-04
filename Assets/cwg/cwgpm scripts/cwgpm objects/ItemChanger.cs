using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SchwerInventory = Schwer.ItemSystem.Inventory;

public class ItemChanger : Interactable
{

    /*this script goes on a Trigger - an empty child with a box collider2D set to trigger. give it two children which are sprites with animators. the first child (0) is the animation of the item changing! the second child (1) is the FX/effect of the item changing (eg sizzling oil).
     * 
     * Cooker1 in the restaurant is an example of the item changer 
     */

    [Header("Inventory")]
    [SerializeField] private Schwer.ItemSystem.InventorySO _inventory = default;
    public SchwerInventory inventory => _inventory.value;

    //[Header("item to be changed")]
    [SerializeField] private Schwer.ItemSystem.Item oldItem = default;

    //[Header("New item")]
    [SerializeField] private Schwer.ItemSystem.Item newItem = default;

    //[Header("New item")]
    [SerializeField] private Schwer.ItemSystem.Item emptySprite = default;


    [Header("Animation")]
   
    public Animator anim;

 
    [Header("Animation")]
    public Animator effect;

    //plays WHILE ITEM is changing
    public string soundEffectToPlay;

    //public BoxCollider2D box;


    // Start is called before the first frame update
    void Start()
    {
       // anim = GetComponent<Animator>();
        anim = gameObject.transform.GetChild (0).GetComponent<Animator>();
        effect = gameObject.transform.GetChild(1).GetComponent<Animator>();
        anim.SetBool("Cooking", false);
        effect.SetBool("Effect", false);
    }


    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (player.inventory[oldItem] > 0)
            {
                anim.SetBool("Cooking", true);
                StartCoroutine(cookingCo());
                Debug.Log("you had the item I was looking for!");
                //put animation here
            }
            else
            {
                Debug.Log("item not found");
            }
        }

    }
    
    IEnumerator cookingCo()
    {

        //BUG fix below
        //makes it so character cannot move during this coroutine
        player.RaiseItem(emptySprite);

        AudioManager.Instance.Play(soundEffectToPlay);
        yield return new WaitForSeconds(0.1f);
        effect.SetBool("Effect", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Cooking", false);
        player.inventory[oldItem]--;
        player.inventory[newItem]++;

        player.RaiseItem(null);

        //AudioManager.instance.Play("ItemGet");
        //not needed: raise item null creates the above sound effect
        effect.SetBool("Effect", false);

        
    }
    
}
