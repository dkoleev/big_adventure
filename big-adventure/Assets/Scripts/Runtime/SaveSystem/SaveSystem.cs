using Runtime.Core;
using Runtime.Events.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;

namespace Runtime.SaveSystem {
    public class SaveSystem : MonoBehaviourSingleton<SaveSystem> {
        [SerializeField]
        private LoadEventChannelSO loadLocationEvent;

        private string saveFilename = "save.biga";
        private string backupSaveFilename = "save.biga.bak";

        private SaveData _saveData = new SaveData();

        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                SaveProgress();
            }
        }

        private void OnApplicationQuit() {
#if UNITY_EDITOR
            SaveProgress();
#endif
        }

        protected override void SingletonEnabled() {
            loadLocationEvent.OnLoadingRequested += OnLocationLoaded;
        }

        protected override void SingletonDisabled() {
            loadLocationEvent.OnLoadingRequested += OnLocationLoaded;
        }

        private void OnLocationLoaded(GameSceneSO location, bool arg1, bool arg2) {
            var locationSO = location as LocationSO;
            if (locationSO) {
                _saveData.locationId = location.Guid;
            } else {
                Debug.LogError("not location loaded by load location channel: " + location.GetType());
            }
        }

        public bool LoadProgress() {
            if (FileManager.LoadFromFile(saveFilename, out var json)) {
                _saveData.LoadFromJson(json);
                return true;
            }

            return false;
        }

        public void SaveProgress() {
            if (FileManager.IsFileExists(saveFilename)) {
                if (FileManager.MoveFile(saveFilename, backupSaveFilename)) {
                    if (FileManager.WriteToFile(saveFilename, _saveData.ToJson())) {
                        Debug.Log("Save successful");
                    }
                }
            } else {
                if (FileManager.WriteToFile(saveFilename, _saveData.ToJson())) {
                    Debug.Log("Save successful");
                }
            }
        }

        public void ClearProgress() {
            FileManager.WriteToFile(saveFilename, "");
        }
    }
}