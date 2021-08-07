using System;
using Runtime.DebugTools;
using Runtime.Events.ScriptableObjects;
using Runtime.Input;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class SceneLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO gameplayScene;
        [SerializeField] private GameSceneSO gameplayUiScene;
        [SerializeField] private InputReader inputReader;
        [Header("Load Events")]
        [SerializeField] private LoadEventChannelSO loadLocation;
        [SerializeField] private LoadEventChannelSO coldStartupLocation;
        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO onSceneReady;

        private AsyncOperationHandle<SceneInstance> _locationOperationHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayUiLoadingOpHandle;

        private GameSceneSO _sceneToLoad;
        private GameSceneSO _currentlyLoadedScene;

        private SceneInstance _gameplayManagerSceneInstance;
        private SceneInstance _gameplayUiSceneInstance;
        private SceneInstance _locationSceneInstance;

        private bool _isLoading = false; //To prevent a new loading request while already loading a new scene

        private void OnEnable() {
            loadLocation.OnLoadingRequested += LoadLocation;
#if UNITY_EDITOR
            coldStartupLocation.OnLoadingRequested += ColdStartupLocation;
#endif
        }

        private void OnDisable() {
            loadLocation.OnLoadingRequested -= LoadLocation;
#if UNITY_EDITOR
            coldStartupLocation.OnLoadingRequested -= ColdStartupLocation;
#endif
        }

        public void RiseLoadLocationEvent(GameSceneSO locationToLoad) {
            loadLocation.RaiseEvent(locationToLoad);
        }

        public void LoadLocation(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen) {
            if (_isLoading)
                return;

            _sceneToLoad = locationToLoad;
            _isLoading = true;

            if (!_gameplayManagerSceneInstance.Scene.isLoaded) {
                LoadGameplayScene(() => {
                    LoadGameplayUiScene(() => {
                        LoadLocationScene();
                    });
                });
            } else {
                LoadLocationScene();
            }
        }
        
        private void ColdStartupLocation(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen) {
            _currentlyLoadedScene = locationToLoad;

            if (_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Location) {
                //Gameplay managers is loaded synchronously
                _gameplayManagerLoadingOpHandle = gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
                _gameplayManagerLoadingOpHandle.WaitForCompletion();
                _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

                _gameplayUiLoadingOpHandle = gameplayUiScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
                _gameplayUiLoadingOpHandle.WaitForCompletion();
                _gameplayUiSceneInstance = _gameplayUiLoadingOpHandle.Result;

                StartGameplay();
                
#if UNITY_EDITOR
                Logs.Log("Cold startup scene: " + locationToLoad.sceneReference.editorAsset.name);
#endif
            }
        }
        
        private void LoadGameplayScene(Action onLoaded = null) {
            _gameplayManagerLoadingOpHandle = gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayManagerLoadingOpHandle.Completed += operationHandle => {
                _gameplayManagerSceneInstance = operationHandle.Result;
                onLoaded?.Invoke();
            };
        }

        private void LoadGameplayUiScene(Action onLoaded = null) {
            _gameplayUiLoadingOpHandle = gameplayUiScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayUiLoadingOpHandle.Completed += operationHandle => {
                _gameplayUiSceneInstance = operationHandle.Result;
                onLoaded?.Invoke();
            };
        }

        private void LoadLocationScene(Action onLoaded = null) {
            UnloadCurrentLocation();
            _locationOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
            _locationOperationHandle.Completed += handle => {
                _currentlyLoadedScene = _sceneToLoad;

                Scene s = handle.Result.Scene;
                SceneManager.SetActiveScene(s);
                _isLoading = false;
                
                StartGameplay();
            };
        }

        private void StartGameplay() {
            inputReader.EnableAllInput();
            onSceneReady.RaiseEvent(); //Spawn system will spawn the PigChef in a gameplay scene
        }

        private void UnloadCurrentLocation() {
            inputReader.DisableAllInput();
            UnloadScene(_currentlyLoadedScene);
        }

        private void UnloadScene(GameSceneSO sceneToUnload) {
            if (sceneToUnload != null) //would be null if the game was started in Initialisation
            {
                if (sceneToUnload.sceneReference.OperationHandle.IsValid()) {
                    //Unload the scene through its AssetReference, i.e. through the Addressable system
                    sceneToUnload.sceneReference.UnLoadScene();
                }
#if UNITY_EDITOR
                else {
                    //Only used when, after a "cold start", the player moves to a new scene
                    //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                    //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                    SceneManager.UnloadSceneAsync(sceneToUnload.sceneReference.editorAsset.name);
                }
#endif
            }
        }
    }
}