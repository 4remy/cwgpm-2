using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SchwerInventory = Schwer.ItemSystem.Inventory;

public class ItemChecker : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private Schwer.ItemSystem.InventorySO _inventory = default;
    public SchwerInventory inventory => _inventory.value;

    //[Header("item you are checking")]
    [SerializeField] private Schwer.ItemSystem.Item oldItem = default;

    public bool playerInRange;

    public GameObject dialogBox;
    public Text dialogText;
    [Multiline]
    public string dialog;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventory[oldItem] > 0)
            {
                Debug.Log("You can cook now");
                playerInRange = true;

                dialogBox.SetActive(true);
                dialogText.text = dialog;

                //i would substitue this for a gameobject thats like an exclamation mark over the grill you turn on and off
            }

            else
            {
                Debug.Log("fuck you no meat");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left range");
            playerInRange = false;

            dialogBox.SetActive(false);

        }
    }
}