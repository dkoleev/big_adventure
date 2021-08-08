using System.Collections.Generic;
using UnityEngine;

namespace Runtime.InventorySystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ItemRecipe", menuName = "Inventory/Recipe")]
    public class ItemRecipeSO : ItemSO {
        [Tooltip("The list of the ingredients necessary to the recipe")]
        [SerializeField]
        private List<ItemStack> _ingredientsList = new List<ItemStack>();

        [Tooltip("The resulting dish to the recipe")]
        [SerializeField]
        private ItemSO _resultingDish = default;


        public override List<ItemStack> IngredientsList => _ingredientsList;
        public override ItemSO ResultingDish => _resultingDish;
    }
}
