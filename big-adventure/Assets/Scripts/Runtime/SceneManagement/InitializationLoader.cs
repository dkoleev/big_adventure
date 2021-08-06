using Runtime.Events.ScriptableObjects;
using Runtime.SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.SceneManagement {
    public class InitializationLoader : MonoBehaviour {
        [SerializeField] private GameSceneSO managersScene;
        [SerializeField] private GameSceneSO locationScene;
        [Header("Load Events")]
        [SerializeField] private LoadEventChannelSO loadLocation;

        private void Start() {
            managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += handle => {
                loadLocation.RaiseEvent(locationScene);
                SceneManager.UnloadSceneAsync(0);
            };
        }
    }
}