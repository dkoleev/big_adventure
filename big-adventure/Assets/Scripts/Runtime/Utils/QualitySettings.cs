using UnityEngine;

namespace Runtime.Utils {
    public class QualitySettings : MonoBehaviour {
        [SerializeField] private int targetFrameRate = 60;
        void Start() {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}