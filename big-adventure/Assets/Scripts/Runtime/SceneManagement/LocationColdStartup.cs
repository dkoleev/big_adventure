using Runtime.Events.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class LocationColdStartup : MonoBehaviour {
        [SerializeField] private GameSceneSO thisSceneSO;
        [SerializeField] private GameSceneSO persistentManagersSO;
        [SerializeField] private AssetReference notifyColdStartupChannel;

        private bool _isColdStart = false;

        private void Awake() {
#if UNITY_EDITOR
            if (!SceneManager.GetSceneByName(persistentManagersSO.sceneReference.editorAsset.name).isLoaded) {
                _isColdStart = true;
            }
#endif
        }

        private void Start() {
            if (_isColdStart) {
                persistentManagersSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed +=
                    LoadEventChannel;
            }
        }

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj) {
            notifyColdStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += handle => {
                handle.Result.RaiseEvent(thisSceneSO);
            };
        }
    }
}