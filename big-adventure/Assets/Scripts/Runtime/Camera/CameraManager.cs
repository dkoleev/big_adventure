using Cinemachine;
using Runtime.Events.ScriptableObjects;
using Runtime.Input;
using UnityEngine;

namespace Runtime.Camera {
    public class CameraManager : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        public UnityEngine.Camera mainCamera;
        public CinemachineVirtualCamera virtualCamera;
        
        [Header("Listening on channels")]
        [Tooltip("The CameraManager listens to this event, fired by objects in any scene, to adapt camera position")]
        [SerializeField] private TransformEventChannelSO frameObjectChannel = default;

        private void Start() {
            
        }

        private void OnEnable() {
            frameObjectChannel.OnEventRaised += OnFrameObjectEvent;
        }
        
        private void OnDisable() {
            frameObjectChannel.OnEventRaised -= OnFrameObjectEvent;
        }

        private void OnFrameObjectEvent(Transform frameObjectTransform) {
            SetupProtagonistVirtualCamera(frameObjectTransform);
        }
        
        private void SetupProtagonistVirtualCamera(Transform target)
        {
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
            virtualCamera.OnTargetObjectWarped(target, target.position - virtualCamera.transform.position - Vector3.forward);
        }
    }
}