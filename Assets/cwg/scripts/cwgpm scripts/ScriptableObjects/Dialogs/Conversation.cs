using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    public Character character;
    //animation clip not connected to sprite
    //
    //public AnimationClip anim;
    //RuntimeAnimationController
    //animator set trigger
    /*public struct EmotionType
    {
        tired,
        sad,
        specific
    }
    */
    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}
