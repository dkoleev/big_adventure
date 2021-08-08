using System;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.InventorySystem {
    [Serializable]
    public class ItemStack {
        [SerializeField]
        private ItemSO _item;

        public ItemSO Item => _item;

        public int Amount {
            get => _amount;
            set => _amount = value;
        }

        private int _amount;

        public ItemStack() {
            _item = null;
            _amount = 0;
        }

        public ItemStack(ItemStack itemStack) {
            _item = itemStack.Item;
            _amount = itemStack._amount;
        }

        public ItemStack(ItemSO item, int amount) {
            _item = item;
            _amount = amount;
        }
    }
}