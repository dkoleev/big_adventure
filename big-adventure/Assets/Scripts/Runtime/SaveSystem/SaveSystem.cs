using System.Collections;
using Runtime.Core;
using Runtime.Events.ScriptableObjects;
using Runtime.InventorySystem.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.SaveSystem {
    public class SaveSystem : MonoBehaviourSingleton<SaveSystem> {
        [SerializeField] private InventorySO playerInventory = default;
        
        [SerializeField]
        private LoadEventChannelSO loadLocationEvent;

        private static readonly string saveFilename = "save.biga";
        private static readonly string backupSaveFilename = "save.biga.bak";

        public SaveData SaveData => _saveData;

        private readonly SaveData _saveData = new SaveData();

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

        private bool LoadSavedFile() {
            if (FileManager.LoadFromFile(saveFilename, out var json)) {
                _saveData.LoadFromJson(json);
                return true;
            }

            return false;
        }

        private void SaveProgress() {
            _saveData.itemStacks.Clear();
            foreach (var itemStack in playerInventory.Items) {
                _saveData.itemStacks.Add(new SerializedItemStack(itemStack.Item.Guid, itemStack.Amount));
            }

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

        public IEnumerator LoadSavedGame() {
            LoadSavedFile();
            
            playerInventory.Items.Clear();
            foreach (var serializedItemStack in _saveData.itemStacks) {
                var loadItemOperationHandle = Addressables.LoadAssetAsync<ItemSO>(serializedItemStack.ItemGuid);
                yield return loadItemOperationHandle;
                
                if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded) {
                    var itemSO = loadItemOperationHandle.Result;
                    playerInventory.Add(itemSO, serializedItemStack.Amount);
                }
            }
        }

        public static void ClearProgress() {
            FileManager.WriteToFile(saveFilename, "");
        }
    }
}