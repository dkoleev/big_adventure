using System;
using Runtime.DebugTools;
using Runtime.Input;
using UnityEngine;

namespace Runtime.Interaction {
    public class InteractionManager : MonoBehaviour {
        [SerializeField] private InputReader inputReader;

        private void OnEnable() {
            inputReader.InteractEvent += Interact;
        }

        private void OnDisable() {
            inputReader.InteractEvent -= Interact;
        }

        private void Interact() {
            Logs.Log("interact");
        }
    }
}