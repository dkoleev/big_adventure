using System.Linq;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class InitializationLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO managersScene;
        [SerializeField] private GameSceneSO locationScene;

        private void Start() {
            Application.targetFrameRate = 60;
            
            managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += handle => {
                var sceneLoader = handle.Result.Scene.GetRootGameObjects().First(go => go.GetComponent<SceneLoader>() != null);
                sceneLoader.GetComponent<SceneLoader>().LoadLocation(locationScene, false, false);
                SceneManager.UnloadSceneAsync(0);
            };
        }
    }
}