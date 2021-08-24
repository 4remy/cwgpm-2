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
            //FindObjectOfType<SoundManager>().Play("itemget");
            player.inventory[item]++;

            //FindObjectOfType<AudioManager>().Play("ItemGet");
            FindObjectOfType<AudioManager>().Play(soundEffectToPlay);

            Destroy(this.gameObject);

            // if you put the sound after destroy it wont play lol
          //  GetComponent<SoundManager>().Play("itemget")
        }
    }
}
