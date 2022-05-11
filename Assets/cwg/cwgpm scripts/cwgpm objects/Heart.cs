using UnityEngine;

public class Heart : powerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    public string soundEffectToPlay;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            AudioManager.Instance.Play(soundEffectToPlay);
            playerHealth.RuntimeValue += amountToIncrease;
            if(playerHealth.RuntimeValue > heartContainers.RuntimeValue * 2f)
            {
                playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
