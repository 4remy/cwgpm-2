using UnityEngine;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Scriptable Object/Item System/Item")]
    public class Item : ScriptableObject {
        [SerializeField] private int _id = default;
        public int id => _id;
        [SerializeField] private string _name = default;
        public new string name => _name;
        [SerializeField][TextArea] private string _description = default;
        public string description => _description;
        [SerializeField] private Sprite _sprite = default;
        public Sprite sprite => _sprite;
        [SerializeField] private bool _stackable = true;
        public bool stackable => _stackable;
        [SerializeField] private bool _usable = false;
        public bool usable => _usable;
        [SerializeField] private bool _unique = false;
        public bool unique => _unique;

          public void Use ()
    {

            Debug.Log("Using " + itemName);
            thisEvent.Invoke();

    }

            public void DecreaseAmount(int amountToDecrease)
    {
        numberHeld -= amountToDecrease;
        if (numberHeld< 0)
        {
            numberHeld = 0;
        }
    }
        
    }
}
