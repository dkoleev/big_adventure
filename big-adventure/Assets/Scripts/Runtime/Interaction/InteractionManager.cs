using System.Collections.Generic;
using JetBrains.Annotations;
using Runtime.Events.ScriptableObjects;
using Runtime.Input;
using Runtime.InventorySystem;
using UnityEngine;

namespace Runtime.Interaction {
    public class InteractionManager : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [Header("Broadcasting on")]
        [SerializeField] private ItemEventChannelSO onObjectPickUp = default;

        private LinkedList<Interaction> _potentialInteractions = new LinkedList<Interaction>();

        public enum InteractionType {
            None = 0,
            PickUp,
            Cook,
            Talk
        };

        private void OnEnable() {
            inputReader.InteractEvent += Interact;
        }

        private void OnDisable() {
            inputReader.InteractEvent -= Interact;
        }

        //Called by the Event on the trigger collider on the child GO called "InteractionDetector"
        [UsedImplicitly]
        public void OnTriggerChangeDetected(bool entered, GameObject obj) {
            var interactable = obj.GetComponent<InteractableObject>();
            if (interactable is null) {
                return;
            }

            if (entered)
                AddPotentialInteraction(interactable);
            else
                RemovePotentialInteraction(interactable);
        }

        private void AddPotentialInteraction(InteractableObject obj) {
            var newPotentialInteraction = new Interaction(InteractionType.PickUp, obj);
            _potentialInteractions.AddFirst(newPotentialInteraction);
        }

        private void RemovePotentialInteraction(InteractableObject obj) {
            var currentNode = _potentialInteractions.First;
            while (currentNode != null) {
                if (currentNode.Value.InteractableObject == obj) {
                    _potentialInteractions.Remove(currentNode);
                    break;
                }

                currentNode = currentNode.Next;
            }
        }

        private void Interact() {
            if (_potentialInteractions.Count == 0)
                return;
            
            var itemObject = _potentialInteractions.First.Value.InteractableObject;
            _potentialInteractions.RemoveFirst();
            var currentItem = itemObject.Interact();
            onObjectPickUp.RaiseEvent(currentItem);
        }
    }
}