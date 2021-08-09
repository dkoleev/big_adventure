using System.Collections;
using Runtime.Events.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.SaveSystem {
    public class ProgressLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO locationSceneDefault;
        [SerializeField] private LoadEventChannelSO loadLocationEvent;

        private IEnumerator Start() {
            yield return SaveSystem.Instance.LoadSavedGame();
            var locationGuid = SaveSystem.Instance.SaveData.locationId;

            if (string.IsNullOrEmpty(locationGuid)) {
                locationGuid = locationSceneDefault.Guid;
            }
            
            var asyncOperationHandle = Addressables.LoadAssetAsync<LocationSO>(locationGuid);

            yield return asyncOperationHandle;

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
                var locationSO = asyncOperationHandle.Result;
                loadLocationEvent.RaiseEvent(locationSO);
            }
        }
    }
}