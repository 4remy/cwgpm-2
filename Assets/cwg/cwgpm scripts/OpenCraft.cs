using Schwer.ItemSystem;
using UnityEngine;

public class OpenCraft : Interactable
{
    [SerializeField] private RecipeList recipeList = default;

    public BoxCollider2D physicsCollider;

    public string soundEffectToPlay;

    protected override void Interact()
    {
        AudioManager.Instance.Play(soundEffectToPlay);

        CraftingManager.RequestCraftingMenu(recipeList);
    }
}
