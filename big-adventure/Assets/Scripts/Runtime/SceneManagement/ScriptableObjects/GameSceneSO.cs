using Common;
using UnityEngine.AddressableAssets;

namespace SceneManagement.ScriptableObjects {
    public class GameSceneSO : DescriptionBaseSO {
        public GameSceneType sceneType;
        public AssetReference sceneReference;

        public enum GameSceneType {
            Location,
            Menu,
            //Special scenes
            Initialisation,
            PersistentManagers,
            Gameplay,
        }
    }
}