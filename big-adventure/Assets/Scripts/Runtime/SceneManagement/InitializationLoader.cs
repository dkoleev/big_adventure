using System.Linq;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class InitializationLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO managersScene;
        [SerializeField] private GameSceneSO locationScene;

        private void Start() {
            managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += handle => {
                var sceneLoader = handle.Result.Scene.GetRootGameObjects().First(go => go.GetComponent<SceneLoader>() != null);
                var loader = sceneLoader.GetComponent<SceneLoader>();
                loader.LoadLocation(locationScene, false, false);
                loader.RiseLoadLocationEvent(locationScene);
                SceneManager.UnloadSceneAsync(0);
            };
        }
    }
}