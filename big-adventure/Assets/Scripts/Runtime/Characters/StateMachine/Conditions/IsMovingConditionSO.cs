using Runtime.StateMachine.Core;
using Runtime.StateMachine.ScriptableObjects;
using UnityEngine;

namespace Runtime.Characters.StateMachine.Conditions {
    [CreateAssetMenu(menuName = "State Machines/Conditions/Started Moving")]
    public class IsMovingConditionSO : StateConditionSO<IsMovingCondition> {
        public float treshold = 0.02f;
    }

    public class IsMovingCondition : Condition {
        private Protagonist _protagonistScript;

        private IsMovingConditionSO _originSO =>
            (IsMovingConditionSO) base.OriginSO; // The SO this Condition spawned from

        public override void Awake(Runtime.StateMachine.Core.StateMachine stateMachine) {
            _protagonistScript = stateMachine.GetComponent<Protagonist>();
        }

        protected override bool Statement() {
            return _protagonistScript.MovementInput.sqrMagnitude > _originSO.treshold;
        }
    }
}