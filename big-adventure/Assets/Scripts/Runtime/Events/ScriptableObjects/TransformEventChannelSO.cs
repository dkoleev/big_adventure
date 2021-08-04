using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Transform Event Channel")]
    public class TransformEventChannelSO : EventChannelBaseSO {
        public UnityAction<Transform> OnEventRaised;

        public void RaiseEvent(Transform value) {
            OnEventRaised?.Invoke(value);
        }
    }
}