using System.Collections.Generic;
using Runtime.Core;
using UnityEngine;
using UnityEngine.Localization;

namespace Runtime.InventorySystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : SerializableScriptableObject {
        [Tooltip("The name of the item")]
        [SerializeField] private LocalizedString itemName = default;
        [Tooltip("A description of the item")]
        [SerializeField] private LocalizedString description = default;
        [Tooltip("A preview image for the item")]
        [SerializeField] private Sprite previewImage = default;
        [Tooltip("The type of item")]
        [SerializeField] private ItemTypeSO itemType = default;
        [Tooltip("A prefab reference for the model of the item")]
        [SerializeField] private GameObject prefab = default;

        public LocalizedString Name => itemName;
        public LocalizedString Description => description;
        public Sprite PreviewImage => previewImage;
        public ItemTypeSO ItemType => itemType;
        public GameObject Prefab => prefab;
        public virtual List<ItemStack> IngredientsList { get;}
        public virtual ItemSO ResultingDish { get; }

        public virtual bool IsLocalized { get; }
        //public virtual LocalizedSprite LocalizePreviewImage { get; }
    }
}
