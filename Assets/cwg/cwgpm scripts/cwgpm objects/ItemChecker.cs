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

    [HideInInspector]
    public bool playerInRange;

    [Header("Show a Dialog or a Sprite?")]
    public bool noDialog;
    public bool noSprite;

    [Header("Sprite to be Shown")]
    //make an empty child gameobject with a sprite renderer
    public GameObject chosenSprite;

    [Header("Normal Dialog box goes here")]
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

                if (!noDialog)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialog;
                    Debug.Log("dialog box showing");
                }

                if(!noSprite)
                {
                    chosenSprite.SetActive(true);
                    Debug.Log("sprite showing");
                }

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
            chosenSprite.SetActive(false);

        }
    }
}