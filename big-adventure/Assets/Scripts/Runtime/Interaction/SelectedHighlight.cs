using System;
using UnityEngine;

namespace Runtime.Interaction {
    public class SelectedHighlight : MonoBehaviour {
        [SerializeField] private GameObject visualRoot;
        
        private bool IsActive => visualRoot.activeSelf;

        private void Awake() {
            visualRoot.SetActive(false);
        }

        public void SetPosition(Vector3 position) {
            if (!IsActive) {
                return;
            }

            transform.position = position;
        }

        public void SetActive(bool isActive) {
            if (isActive == IsActive) {
                return;
            }

            visualRoot.SetActive(isActive);
        }
    }
}