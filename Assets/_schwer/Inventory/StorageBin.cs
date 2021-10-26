using Schwer.ItemSystem;
using UnityEngine;

public class StorageBin : Interactable
{
    [SerializeField] private InventorySO storage = default;

    protected override void Interact()
    {
        if (player.state != CharacterController2D.State.Interact)
        {
            InventoryStorageManager.Request(player.inventory, storage.value);
        }
    }
}
