using UnityEngine;


public class itemDropNoRespawn : MonoBehaviour
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;
    public string soundEffectToPlay;
    public bool isGone;
    public BoolValue storedGone;


    private void Start()
    {
        isGone = storedGone.RuntimeValue;
        if (isGone)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            //AudioManager.instance.Play("itemget");
            player.inventory[item]++;

            //AudioManager.instance.Play("ItemGet");
            isGone = true;
            storedGone.RuntimeValue = isGone; AudioManager.Instance.Play(soundEffectToPlay);

            Destroy(this.gameObject);

            // if you put the sound after destroy it wont play lol
            //  GetComponent<AudioManager>().Play("itemget")
        }
    }
}
