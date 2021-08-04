using System;
using Runtime.Characters;
using Runtime.Events.ScriptableObjects;
using UnityEngine;

namespace Runtime.Gameplay {
    public class SpawnSystem : MonoBehaviour {
        [Header("Asset References")]
        [SerializeField] private Protagonist playerPrefab;
        [Header("Scene References")]
        [SerializeField]private Transform defaultSpawnPoint;
        
        [SerializeField] private TransformEventChannelSO playerInstantiatedChannel = default;

        [Header("Scene Ready Event")]
        [SerializeField] private VoidEventChannelSO onSceneReady;

        private Protagonist _player;

        private void Start() {
        }
        
        private void OnEnable()
        {
            onSceneReady.OnEventRaised += SpawnPlayer;
        }

        private void OnDisable()
        {
            onSceneReady.OnEventRaised -= SpawnPlayer;
        }

        private void SpawnPlayer() {
            _player = InstantiatePlayer(playerPrefab, GetSpawnLocation());
            playerInstantiatedChannel.RaiseEvent(_player.transform);
        }

        private Protagonist InstantiatePlayer(Protagonist prefab, Transform spawnLocation)
        {
            if (playerPrefab == null)
                throw new Exception("Player Prefab can't be null.");

            Protagonist playerInstance = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);

            return playerInstance;
        }

        private Transform GetSpawnLocation() {
            return defaultSpawnPoint;
        }
    }
}