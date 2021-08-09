using System;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.InventorySystem {
    [Serializable]
    public class ItemStack {
        [SerializeField]
        private ItemSO item;
        [SerializeField]
        private int amount;
        
        public ItemSO Item => item;

        public int Amount {
            get => amount;
            set => amount = value;
        }

        public ItemStack() {
            item = null;
            amount = 0;
        }

        public ItemStack(ItemStack itemStack) {
            item = itemStack.Item;
            amount = itemStack.amount;
        }

        public ItemStack(ItemSO item, int amount) {
            this.item = item;
            this.amount = amount;
        }
    }
}