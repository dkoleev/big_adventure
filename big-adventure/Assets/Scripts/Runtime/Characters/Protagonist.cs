using Runtime.Input;
using UnityEngine;

namespace Runtime.Characters {
    public class Protagonist : MonoBehaviour {
        [SerializeField] private InputReader inputReader = default;
        [SerializeField] private Transform rotateRoot;
        [SerializeField] private Animator baseAnimator;

        public Vector3 MovementInput => _inputVector;
        public Animator BaseAnimator => baseAnimator;
        public Transform RotateRoot => rotateRoot;
        
        private Vector2 _inputVector = Vector2.zero;
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
    }
}