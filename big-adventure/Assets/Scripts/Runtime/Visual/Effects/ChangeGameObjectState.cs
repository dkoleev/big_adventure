using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Visual.Effects {
    public class ChangeGameObjectState : MonoBehaviour {
        [SerializeField] private List<GameObject> states;
        
        private Queue<GameObject> _statesQueue = new Queue<GameObject>();
        private GameObject _currentState;

        private void Awake() {
            foreach (var state in states) {
                state.SetActive(false);
                _statesQueue.Enqueue(state);
            }
            
            _statesQueue.Peek().SetActive(true);
        }

        public void SetNextState() {
            if (_statesQueue.Peek() == null) {
                return;
            }

            _statesQueue.Dequeue().SetActive(false);
            
            if (_statesQueue.Peek() != null) {
                _statesQueue.Peek().SetActive(true);
            }
        }
    }
}