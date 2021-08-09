using Runtime.Events.UnityEvents;
using UnityEngine;

namespace Runtime.Characters {
    public class ZoneTriggerController : MonoBehaviour {
        [SerializeField]
        private BoolEventUnity enterZone = default;

        [SerializeField]
        private LayerMask layers = default;

        private void OnTriggerEnter(Collider other) {
            if ((1 << other.gameObject.layer & layers) != 0) {
                enterZone.Invoke(true, other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) {
            if ((1 << other.gameObject.layer & layers) != 0) {
                enterZone.Invoke(false, other.gameObject);
            }
        }
    }
}