using System.Collections.Generic;
using Runtime.Events.UnityEvents;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.Interaction {
    public class InteractableObject : MonoBehaviour {
        [SerializeField] private ItemSO currentItem = default;
        [SerializeField] private List<ItemSO> reward = default;
        [SerializeField] private EmptyEventUnity onInteractEvent;

        public bool CanInteract => _canInteract;

        private bool _canInteract = true;

        public ItemSO Interact() {
            _canInteract = false;
            
            onInteractEvent?.Invoke(gameObject);
            
            return currentItem;
        }
    }
}