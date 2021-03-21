using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    enum State
    {
        happy, sad, angry
    }

    State state = State.happy;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        //automatically starts as happy
        switch (state)
        {
            case State.happy:
                happyState();
                break;
            case State.sad:
                sadSate();
                break;
            case State.angry:
                angrySate();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
                sadSate();
        }
    }

    public void happyState()
    {
        animator.SetBool("happy", true);
        // no debug log to say 'happy', because its the default emotion, AND on a fixed update. it will spam you in updates that it's happy, for every time it does a check at a fixed update of time
    }

    public void sadSate()
    {
        animator.SetBool("sad", true);
        Debug.Log("emotion changed to sad");
    }

    public void angrySate()
    {
        animator.SetBool("angry", true);
        Debug.Log("emotion changed to angry");
    }
}

//this works in tandem with an animaton controller and extends its potential applications
// bc it can say 'set the animator' and have a condition for like, if you have X inventory item, and THEN change emotion if you fulfill that.
//seems like you might want to use this on the character bust if you end up building an empty game object, with a sprite displayer on it, and put that in the dialogue box prefab on the UI canvas.
//then you could work out how to 'set' the emotion in the dialogue.



//this is a manager for the animation controller
//the animation controller is a type of state machine
