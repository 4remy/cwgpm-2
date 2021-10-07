using Schwer.ItemSystem;
using UnityEngine;

public class OpenCraft : Interactable
{
    [SerializeField] private RecipeList recipeList = default;

    protected override void Interact()
    {
        if (player.state != CharacterController2D.State.Interact)
        {
            CraftingManager.RequestCraftingMenu(recipeList);
        }
    }
}
