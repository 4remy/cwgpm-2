using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A choice in a conversation script.
    /// </summary>
    [System.Serializable]
    public struct ConversationOption
    {
        public string text;
        public Sprite image;
        public AudioClip audio;
        public string targetId;
        public bool enabled;
        //start of addition
        public enum iconAlignmnent
        {
            Left,
            Center,
            Right,

        }
        public iconAlignmnent theAlignment;
        //end of addition

    }
}