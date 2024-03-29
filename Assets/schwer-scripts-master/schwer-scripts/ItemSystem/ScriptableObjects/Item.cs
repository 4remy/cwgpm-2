﻿using UnityEngine;

namespace Schwer.ItemSystem {
    using Schwer.Database;

    [CreateAssetMenu(menuName = "Item System/Item")]
    public class Item : ScriptableObject, IID {
        // currentItem here?
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
        [SerializeField] private bool _isKey = false;
        public bool isKey => _isKey;
    }
}
