using System;
using Components;
using Leopotam.EcsLite;
using ScriptableObjects;
using Systems;
using UnityEngine;

namespace MonoBehaviours
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Door[] doors;
        private EcsWorld _ecsWorld;
        private EcsSystems initSystems;
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;

        private void Start()
        {
            _ecsWorld = new EcsWorld();
            var gameData = new GameData();
            gameData.doors = new DoorComponent[doors.Length];
            for (int i = 0; i < doors.Length; i++)
            {
                gameData.doors[i].doorTransform = doors[i].doorTransform;
                gameData.doors[i].doorTag = doors[i].doorTag;
                gameData.doors[i].openedPosition = doors[i].openedPosition;
                gameData.doors[i].speedOpening = doors[i].speedOpening;
            }
            
            initSystems = new EcsSystems(_ecsWorld, gameData);
            initSystems.Add(new PlayerInitSystem());
            initSystems.Add(new DoorsInitSystem());
            initSystems.Init();
            
            updateSystems = new EcsSystems(_ecsWorld, gameData);
            updateSystems.Add(new PlayerInputSystem());
            updateSystems.Add(new DoorOpeningSystem());
            updateSystems.Init();
            
            fixedUpdateSystems = new EcsSystems(_ecsWorld);
            fixedUpdateSystems.Add(new PlayerMovableSystem());
            fixedUpdateSystems.Init();
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            initSystems.Destroy();
            updateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            
            _ecsWorld.Destroy();
        }
    }

    [Serializable]
    public class Door
    {
        public Transform doorTransform;
        public string doorTag;
        public float speedOpening;
        public Vector3 openedPosition;
    }
}