using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Input {
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions {
        // Assign delegate{} to events to initialise them with an empty delegate
        // so we can skip the null check when we use them

        // Gameplay
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction ClickEvent = delegate { };
        
        private GameInput _gameInput;

        private void OnEnable() {
            if (_gameInput == null) {
                _gameInput = new GameInput();
                _gameInput.UI.SetCallbacks(this);
                _gameInput.Player.SetCallbacks(this);
                
                SetActiveGameplayInput(true);
                SetActiveUIInput(true);
            }
        }

        private void OnDisable() {
           DisableAllInput();
        }

        public void SetActiveUIInput(bool isActive) {
            if (isActive) {
                _gameInput.UI.Enable();
            } else {
                _gameInput.UI.Disable();
            }
        }

        public void SetActiveGameplayInput(bool isActive) {
            if (isActive) {
                _gameInput.Player.Enable();
            } else {
                _gameInput.Player.Disable();
            }
        }

        public void DisableAllInput() {
            _gameInput.Player.Disable();
            _gameInput.UI.Disable();
        }
        
        public void EnableAllInput() {
            _gameInput.Player.Enable();
            _gameInput.UI.Enable();
        }

        #region Gameplay actions

        public void OnMove(InputAction.CallbackContext context) {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context) {
        }

        public void OnFire(InputAction.CallbackContext context) {
        }

        #endregion

        #region UI actions

        public void OnNavigate(InputAction.CallbackContext context) {
        }

        public void OnSubmit(InputAction.CallbackContext context) {
        }

        public void OnCancel(InputAction.CallbackContext context) {
        }

        public void OnPoint(InputAction.CallbackContext context) {
        }

        public void OnClick(InputAction.CallbackContext context) {
            ClickEvent.Invoke();
        }

        public void OnScrollWheel(InputAction.CallbackContext context) {
        }

        public void OnMiddleClick(InputAction.CallbackContext context) {
        }

        public void OnRightClick(InputAction.CallbackContext context) {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context) {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) {
        }

        public void OnTrackedDeviceSelect(InputAction.CallbackContext context) {
        }

        #endregion
    }
}