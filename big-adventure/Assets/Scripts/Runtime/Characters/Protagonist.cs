using System.Diagnostics;
using Runtime.Input;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Runtime.Characters {
    public class Protagonist : MonoBehaviour {
        [SerializeField] private InputReader inputReader = default;
        [SerializeField] private float speedMove = 3f;
        [SerializeField] private Transform rotateRoot;
        [SerializeField] private float speedRotate = 5f;

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

        private void Update() {
            Move();
            RotateByMoveDirection();
        }

        private void OnMove(Vector2 movement) {
            _inputVector = movement;
        }
        
        private void Move() {
            if (_inputVector.magnitude > 0) {
                var speedModifier = Mathf.Clamp01(_inputVector.magnitude);
                
                transform.localPosition += new Vector3(
                    _inputVector.x * Time.deltaTime * speedMove * speedModifier,
                    0,
                    _inputVector.y * Time.deltaTime * speedMove * speedModifier);
            }
        }

        private void RotateByMoveDirection() {
            if (_inputVector.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(_inputVector.x, 0, _inputVector.y);
            if (move.magnitude > 1f) {
                move.Normalize();
            }

            var forward = rotateRoot.forward;
            var angleCurrent = Mathf.Atan2( forward.x, forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            var deltaAngle = Mathf.DeltaAngle(angleCurrent, targetAngle);
            var targetLocalRot = Quaternion.Euler(0, deltaAngle, 0);
            var targetRotation = Quaternion.Slerp(Quaternion.identity, targetLocalRot, speedRotate * Time.deltaTime);

            rotateRoot.rotation *= targetRotation;
        }

        [Conditional("DEBUG")]
        private void Log(string message) {
            Debug.Log(message);
        }
    }
}