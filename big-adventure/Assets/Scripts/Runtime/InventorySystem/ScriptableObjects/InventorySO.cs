using System.Collections.Generic;
using UnityEngine;

namespace Runtime.InventorySystem.ScriptableObjects {
	[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
	public class InventorySO : ScriptableObject {
		[Tooltip("The collection of items and their quantities.")]
		[SerializeField] private List<ItemStack> _items = new List<ItemStack>();
		[SerializeField] private List<ItemStack> _defaultItems = new List<ItemStack>();

		public List<ItemStack> Items => _items;
		
		public void Add(ItemSO item, int count = 1) {
			if (count <= 0)
				return;

			foreach (var currentItemStack in _items) {
				if (item == currentItemStack.Item) {
					//only add to the amount if the item is usable 
					if (currentItemStack.Item.ItemType.ActionType == ItemInventoryActionType.Use) {
						currentItemStack.Amount += count;
					}

					return;
				}
			}

			_items.Add(new ItemStack(item, count));
		}

		public void Remove(ItemSO item, int count = 1) {
			if (count <= 0)
				return;

			for (int i = 0; i < _items.Count; i++) {
				ItemStack currentItemStack = _items[i];

				if (currentItemStack.Item != item) {
					continue;
				}

				currentItemStack.Amount -= count;

				if (currentItemStack.Amount <= 0) {
					_items.Remove(currentItemStack);
				}

				return;
			}
		}

		public bool Contains(ItemSO item) {
			foreach (var itemInStack in _items) {
				if (item == itemInStack.Item) {
					return true;
				}
			}

			return false;
		}

		public int Count(ItemSO item) {
			foreach (var currentItemStack in _items) {
				if (item == currentItemStack.Item) {
					return currentItemStack.Amount;
				}
			}

			return 0;
		}

		public bool[] IngredientsAvailability(List<ItemStack> ingredients) {
			if (ingredients == null)
				return null;
			
			bool[] availabilityArray = new bool[ingredients.Count];

			for (int i = 0; i < ingredients.Count; i++) {
				availabilityArray[i] =
					_items.Exists(o => o.Item == ingredients[i].Item && o.Amount >= ingredients[i].Amount);
			}

			return availabilityArray;
		}

		public bool HasIngredients(List<ItemStack> ingredients) {
			bool hasIngredients =
				!ingredients.Exists(j => !_items.Exists(o => o.Item == j.Item && o.Amount >= j.Amount));

			return hasIngredients;
		}
		
		public void ResetToDefault() {
			_items ??= new List<ItemStack>();
			_items.Clear();
			
			foreach (ItemStack item in _defaultItems) {
				_items.Add(new ItemStack(item));
			}
		}
	}
}