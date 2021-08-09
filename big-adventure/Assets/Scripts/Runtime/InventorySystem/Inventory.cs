using System;
using Runtime.Events.ScriptableObjects;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.InventorySystem {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        private InventorySO _currentInventory = default;
        
        [SerializeField] private ItemEventChannelSO addItemEvent = default;

        private void OnEnable() {
            addItemEvent.OnEventRaised += AddItem;
        }
        
        private void OnDisable() {
            addItemEvent.OnEventRaised -= AddItem;
        }

        private void AddItem(ItemSO item) {
            _currentInventory.Add(item);
        }

        private void AddItemStack(ItemStack itemStack) {
            _currentInventory.Add(itemStack.Item, itemStack.Amount);

        }

        private void RemoveItem(ItemSO item) {
            _currentInventory.Remove(item);
        }
    }
}