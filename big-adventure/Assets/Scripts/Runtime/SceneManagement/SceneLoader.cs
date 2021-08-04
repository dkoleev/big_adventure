using System;
using Runtime.Events.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] private GameplaySO gameplayScene;
        [SerializeField] private GameSceneSO gameplayUiScene;
        [SerializeField] private GameSceneSO locationScene;

        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO onSceneReady;
        
        
        private AsyncOperationHandle<SceneInstance> _locationOperationHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayUiLoadingOpHandle;

        private SceneInstance _gameplayManagerSceneInstance;
        private SceneInstance _gameplayUiSceneInstance;
        private SceneInstance _locationSceneInstance;

        private void Start() {
            LoadGameplayScene(() => {
                LoadLocationScene(() => {
                    LoadGameplayUiScene(() => {
                        SceneManager.SetActiveScene(_locationSceneInstance.Scene);
                        onSceneReady.RaiseEvent();
                    });
                });
            });
        }

        private void LoadGameplayUiScene(Action onLoaded = null) {
            _gameplayUiLoadingOpHandle = gameplayUiScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplayUiLoadingOpHandle.Completed += handle => {
                _gameplayUiSceneInstance = handle.Result;
                
                onLoaded?.Invoke();
            };
        }

        private void LoadGameplayScene(Action onLoaded = null) {
            _gameplayManagerLoadingOpHandle = gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplayManagerLoadingOpHandle.Completed += handle => {
                _gameplayManagerSceneInstance = handle.Result;
                
                onLoaded?.Invoke();
            };
        }

        private void LoadLocationScene(Action onLoaded = null) {
            _locationOperationHandle = locationScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _locationOperationHandle.Completed += handle => {
                _locationSceneInstance = handle.Result;
                
                onLoaded?.Invoke();
            };
        }
    }
}