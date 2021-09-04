using UnityEngine;


public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;
    public string soundEffectToPlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            //AudioManager.instance?.Play("itemget");
            player.inventory[item]++;

            //AudioManager.instance?.Play("ItemGet");
            AudioManager.instance?.Play(soundEffectToPlay);

            Destroy(this.gameObject);

            // if you put the sound after destroy it wont play lol
            //  GetComponent<AudioManager>().Play("itemget")
        }
    }
}
