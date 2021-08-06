using Runtime.StateMachine.Core;
using Runtime.StateMachine.ScriptableObjects;
using UnityEngine;

namespace Runtime.Characters.StateMachine.Actions.Animator {
    [CreateAssetMenu(fileName = "AnimatorMoveSpeedAction", menuName = "State Machines/Actions/Animator/Set Animator Move Speed")]
    public class AnimatorMoveSpeedActionSO : StateActionSO {
        public string parameterName = default;

        protected override StateAction CreateAction() =>
            new AnimatorMoveSpeedAction(UnityEngine.Animator.StringToHash(parameterName));
    }

    public class AnimatorMoveSpeedAction : StateAction {
        //Component references
        private UnityEngine.Animator _animator;
        private Protagonist _protagonist;

        private AnimatorMoveSpeedActionSO _originSO =>
            (AnimatorMoveSpeedActionSO) base.OriginSO; // The SO this StateAction spawned from

        private int _parameterHash;

        public AnimatorMoveSpeedAction(int parameterHash) {
            _parameterHash = parameterHash;
        }

        public override void Awake(Runtime.StateMachine.Core.StateMachine stateMachine) {
            _protagonist = stateMachine.GetComponent<Protagonist>();
            _animator = _protagonist.BaseAnimator;
        }

        public override void OnUpdate() {
            //TODO: do we like that we're using the magnitude here, per frame? Can this be done in a smarter way?
            float normalisedSpeed = _protagonist.MovementInput.magnitude;
            _animator.SetFloat(_parameterHash, normalisedSpeed);
        }
    }
}