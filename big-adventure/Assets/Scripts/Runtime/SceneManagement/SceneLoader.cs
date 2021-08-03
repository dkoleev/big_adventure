using System;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] private GameplaySO gameplayScene;
        [SerializeField] private GameSceneSO locationScene;
        
        private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

        private SceneInstance _gameplayManagerSceneInstance;

        private void Start() {
            LoadGameplayScene(() => {
                LoadLocationScene();
            });
        }

        private void LoadGameplayScene(Action onLoaded = null) {
            _gameplayManagerLoadingOpHandle = gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplayManagerLoadingOpHandle.Completed += handle => {
                _gameplayManagerSceneInstance = handle.Result;
                onLoaded?.Invoke();
            };
        }

        private void LoadLocationScene(Action onLoaded = null) {
            _loadingOperationHandle = locationScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _loadingOperationHandle.Completed += handle => {
                onLoaded?.Invoke();
            };
        }
    }
}