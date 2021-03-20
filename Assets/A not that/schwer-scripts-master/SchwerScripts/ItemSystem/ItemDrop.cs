using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.inventory[item]++;
            Destroy(this.gameObject);
        }
    }
}
