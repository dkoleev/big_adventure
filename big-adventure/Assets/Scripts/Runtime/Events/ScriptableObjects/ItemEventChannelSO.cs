using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Interactable/Item Event Channel")]
    public class ItemEventChannelSO : ScriptableObject {
        public UnityAction<ItemSO> OnEventRaised;

        public void RaiseEvent(ItemSO item) {
            OnEventRaised?.Invoke(item);
        }
    }
}