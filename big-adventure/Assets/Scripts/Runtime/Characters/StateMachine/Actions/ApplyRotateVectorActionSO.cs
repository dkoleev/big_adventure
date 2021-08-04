using Runtime.StateMachine.Core;
using Runtime.StateMachine.ScriptableObjects;
using UnityEngine;

namespace Runtime.Characters.StateMachine.Actions {
    [CreateAssetMenu(fileName = "ApplyRotateVector", menuName = "State Machines/Actions/Apply Rotate Vector")]
    public class ApplyRotateVectorActionSO : StateActionSO<ApplyRotateVectorAction>{
        public float speedRotate = 6f;
    }
    
    public class ApplyRotateVectorAction : StateAction {
        //Component references
        private Protagonist _protagonistScript;

        private ApplyRotateVectorActionSO _originSO =>
            (ApplyRotateVectorActionSO) base.OriginSO; // The SO this StateAction spawned from

        public override void Awake(Runtime.StateMachine.Core.StateMachine stateMachine) {
            _protagonistScript = stateMachine.GetComponent<Protagonist>();
        }

        public override void OnUpdate() {
            var vector = _protagonistScript.MovementInput;
            if (vector.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(vector.x, 0, vector.y);
            if (move.magnitude > 1f) {
                move.Normalize();
            }

            var forward = _protagonistScript.RotateRoot.forward;
            var angleCurrent = Mathf.Atan2( forward.x, forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            var deltaAngle = Mathf.DeltaAngle(angleCurrent, targetAngle);
            var targetLocalRot = Quaternion.Euler(0, deltaAngle, 0);
            var targetRotation = Quaternion.Slerp(Quaternion.identity, targetLocalRot, _originSO.speedRotate * Time.deltaTime);

            _protagonistScript.RotateRoot.rotation *= targetRotation;
        }
    }
}
