using Runtime.Events.UnityEvents;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.Interaction {
    public class InteractableObject : MonoBehaviour {
        [SerializeField] private ItemSO currentItem = default;
        [SerializeField] private EmptyEventUnity onInteractEvent;

        public ItemSO Interact() {
            onInteractEvent?.Invoke(gameObject);
            
            return currentItem;
        }
    }
}