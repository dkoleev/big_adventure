using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.InventorySystem {
    public class CollectableItem : MonoBehaviour {
        [SerializeField] private ItemSO currentItem = default;
        [SerializeField] private GameObject itemGo = default;

        private void Start() {
            AnimateItem();
        }

        public ItemSO GetItem() {
            return currentItem;
        }

        public void SetItem(ItemSO item) {
            currentItem = item;
        }

        public void AnimateItem() {
            if (itemGo != null) {
               // _itemGO.transform.DORotate(Vector3.one * 180, 5, RotateMode.Fast).SetLoops(-1, LoopType.Incremental);
            }
        }
    }
}