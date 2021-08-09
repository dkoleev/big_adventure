using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class InitializationLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO managersScene;
       
        private void Start() {
            managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += handle => {
                SceneManager.UnloadSceneAsync(0);
            };
        }
    }
}