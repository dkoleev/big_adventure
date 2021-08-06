using Runtime.StateMachine.Core;
using Runtime.StateMachine.ScriptableObjects;
using UnityEngine;

namespace Runtime.Characters.StateMachine.Actions {
    [CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Apply Movement Vector")]
    public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> {
        public float speedMove = 3f;
    }

    public class ApplyMovementVectorAction : StateAction {
        //Component references
        private Protagonist _protagonistScript;
        private CharacterController _characterController;

        private ApplyMovementVectorActionSO _originSO =>
            (ApplyMovementVectorActionSO) base.OriginSO; // The SO this StateAction spawned from

        public override void Awake(Runtime.StateMachine.Core.StateMachine stateMachine) {
            _protagonistScript = stateMachine.GetComponent<Protagonist>();
            _characterController = stateMachine.GetComponent<CharacterController>();
        }

        public override void OnUpdate() {
            var vector = new Vector3(_protagonistScript.MovementInput.x, 0, _protagonistScript.MovementInput.y);
            var speed = vector * _originSO.speedMove * Time.deltaTime;

            _characterController.Move(speed);
        }
    }
}