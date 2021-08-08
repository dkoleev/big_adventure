using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.InventorySystem {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        private InventorySO _currentInventory = default;

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