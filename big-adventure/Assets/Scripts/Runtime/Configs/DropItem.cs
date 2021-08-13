using System;
using Runtime.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace Runtime.Configs {
    [Serializable]
    public class DropItem {
        [SerializeField] ItemSO item;
        [Range(0,1)]
        [SerializeField] float itemDropRate;

        public ItemSO Item => item;
        public float ItemDropRate => itemDropRate;
    }
}