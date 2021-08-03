using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement {
    public class InitializationLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO managersScene;

        private void Start() {
            managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        }
    }
}