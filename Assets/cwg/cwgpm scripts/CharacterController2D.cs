using System.Collections;
using Schwer.ItemSystem;
using UnityEngine;
using SchwerInventory = Schwer.ItemSystem.Inventory;
using SchwerItem = Schwer.ItemSystem.Item;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 1;
    public float acceleration = 2;

    //public Vector3 nextMoveCommand;
    //added
    [SerializeField] private InventorySO _inventory = default;
    public SchwerInventory inventory => _inventory.value;

    public Vector3 change;
    // ^added
    public Animator animator;
    public bool flipX = false;

    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public float knockTime;
    //public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public VectorValue startingPosition;

    private Rigidbody2D myRigidbody;
    SpriteRenderer spriteRenderer;

    enum State
    {
        Idle, Moving, Interact, Sitting
    }

    State state = State.Idle;
    //  Vector3 start, end;
    //    Vector2 currentVelocity;
    //   float startTime;
    //   float distance;
    //   float velocity;

    void IdleState()
    {
        //would be good if this preserved last direction you faced
        //maybe check taft for this

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("moving", false);
        if (change != Vector3.zero)
        {
            playerHealthSignal.Raise();
            animator.SetBool("moving", true);
            state = State.Moving;

        }
    }

    void MoveState()
    {
        animator.SetBool("moving", true);
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        //animator.SetFloat("WalkX", change.x < 0 ? -1 : change.x > 0 ? 1 : 0);
        //animator.SetFloat("WalkY", change.y < 0 ? 1 : change.y > 0 ? -1 : 0);

        //last = Vector3.zero;
        //last.x = animator.GetFloat("WalkX");
        //last.y = animator.GetFloat("WalkY");

        if (change.x < 0)
        {
            animator.SetFloat("WalkX", -1);
            animator.SetFloat("WalkY", 0);
        }
        if (change.x > 0)
        {
            animator.SetFloat("WalkX", 1);
            animator.SetFloat("WalkY", 0);
        }

        if (change.y < 0)
        {
            animator.SetFloat("WalkY", 1);
            animator.SetFloat("WalkX", 0);
        }
        if (change.y > 0)
        {
            animator.SetFloat("WalkY", -1);
            animator.SetFloat("WalkX", 0);
        }

        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
        if (change == Vector3.zero)
        {
            state = State.Idle;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Moving:
                MoveState();
                break;
            case State.Sitting:
                SittingAnimate();
                break;
            case State.Interact:
                Interact();
                break;
        }
    }

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startingPosition.initialValue;
    }

    public void ConvoStartSignal()
    {
        animator.SetBool("moving", false);
        state = State.Interact;
        change = Vector3.zero;
        Debug.Log("you shouldn't be able to move");
    }

    public void ConvoFinishSignal()
    {
        Debug.Log("convo finished signal recieved");
        state = State.Idle;
    }

    public void RaiseItem(SchwerItem item)
    {

        if (state != State.Interact && item != null)
        {
            animator.SetBool("ReceiveItem", true);
            state = State.Interact;
            //do not use animator.speed = 0; use below
            change = Vector3.zero;
            //previous item system below
            //receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            receivedItemSprite.sprite = item.sprite;
            Debug.Log("should be recieving item");
            AudioManager.Instance.Play("ItemGet");
        }
        else
        {
            animator.SetBool("ReceiveItem", false);
            state = State.Idle;
            receivedItemSprite.sprite = null;
            Debug.Log("finishing recieving item");
           
            //player.inventory.currentItem = null;
        }
    }

    public void SittingAnimate()
    {
        // speed = 0; is bad
        // adding line below instead
        change = Vector3.zero;
        // myRigidbody.velocity = Vector2.zero;
        animator.SetBool("IsSitting", true);
        //
        //move these two vectors to be external arguments in bus stop script
        Vector3 sittingPosition1 = new Vector3(-5, 4, 0);
        gameObject.transform.position = sittingPosition1;
    }

    public void SittingState()
    {
        if (animator.GetBool("IsSitting") == false)
        {
            state = State.Sitting;
            animator.SetBool("IsSitting", true);
        }
        else
        {
            state = State.Idle;
            //move these two vectors to be external arguments in bus stop script
            Vector3 standingPosition1 = new Vector3(-5, 3, 0);
            gameObject.transform.position = standingPosition1;
            animator.SetBool("IsSitting", false);
        }
    }

    public void Interact()
    {
        change = Vector3.zero;
        // this is not stopping movement during dialogue

    }

    public void Knock(float knockTime, float damage)
    {
        playerHealthSignal.Raise();
        currentHealth.RuntimeValue -= damage;
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime, damage));
            AudioManager.Instance.Play("Oink");
        }
        else
        {
            Debug.Log("no cats");
        }
    }

    private IEnumerator KnockCo(float knockTime, float damage)
    {
        {
            yield return new WaitForSeconds(knockTime);
            if (state != State.Interact)
            {
                state = State.Idle;
            }
        }
    }
}
