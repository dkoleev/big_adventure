using System.Diagnostics;
using Runtime.Input;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Runtime.Characters {
    public class Protagonist : MonoBehaviour {
        [SerializeField]
        private InputReader inputReader = default;

        private Vector2 _inputVector;

        private GameInput _gameInput;
        private void Start() {
            _gameInput = new GameInput();
            _gameInput.Player.Enable();
        }

        private void OnEnable() {
            inputReader.MoveEvent += OnMove;
        }

        private void OnDisable() {
            inputReader.MoveEvent -= OnMove;
        }

        private void OnMove(Vector2 movement) {
            _inputVector = movement;
        }

        [Conditional("DEBUG")]
        private void Log(string message) {
            Debug.Log(message);
        }
    }
}