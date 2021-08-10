using System.Collections.Generic;
using JetBrains.Annotations;
using Runtime.Events.ScriptableObjects;
using Runtime.Input;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Interaction {
    public class InteractionManager : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private AssetReference highlightAsset;
        [Header("Broadcasting on")]
        [SerializeField] private ItemEventChannelSO onObjectPickUp = default;

        private readonly LinkedList<Interaction> _potentialInteractions = new LinkedList<Interaction>();
        private SelectedHighlight _highlight;

        public enum InteractionType {
            None = 0,
            PickUp,
            Cook,
            Talk
        };

        private void Start() {
            highlightAsset.LoadAssetAsync<GameObject>().Completed += handle => {
                _highlight = Instantiate(handle.Result).GetComponent<SelectedHighlight>();
            };
        }

        private void OnEnable() {
            inputReader.MoveEvent += OnPlayerMove;
            inputReader.InteractEvent += Interact;
        }
        
        private void OnDisable() {
            inputReader.MoveEvent -= OnPlayerMove;
            inputReader.InteractEvent -= Interact;
        }
        
        private void OnPlayerMove(Vector2 arg0) {
            UpdateHighlight();
        }

        //Called by the Event on the trigger collider on the child GO called "InteractionDetector"
        [UsedImplicitly]
        public void OnTriggerChangeDetected(bool entered, GameObject obj) {
            var interactable = obj.GetComponent<InteractableObject>();
            if (interactable is null) {
                return;
            }

            if (entered) {
                AddPotentialInteraction(interactable);
            } else {
                RemovePotentialInteraction(interactable);
            }

            UpdateHighlight();
        }

        private void AddPotentialInteraction(InteractableObject obj) {
            if (!obj.CanInteract) {
                return;
            }
            
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

        private void UpdateHighlight() {
            if (_highlight is null) {
                return;
            }

            _highlight.SetActive(_potentialInteractions.Count != 0);
            if (_potentialInteractions.Count == 0) {
                return;
            }

            var target = _potentialInteractions.First.Value.InteractableObject;
            _highlight.SetPosition(target.transform.position);
        }

        private void Interact() {
            if (_potentialInteractions.Count == 0)
                return;
            
            var itemObject = _potentialInteractions.First.Value.InteractableObject;
            _potentialInteractions.RemoveFirst();
            var currentItem = itemObject.Interact();
            onObjectPickUp.RaiseEvent(currentItem);

            UpdateHighlight();
        }
    }
}