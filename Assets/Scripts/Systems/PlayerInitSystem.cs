using Components;
using Leopotam.EcsLite;
using MonoBehaviours;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var playerEntity = ecsWorld.NewEntity();
            
            var playerInputComponentPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputComponentPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputComponentPool.Get(playerEntity);
            
            var playerMovePool = ecsWorld.GetPool<PlayerMoveComponent>();
            playerMovePool.Add(playerEntity);
            ref var playerMoveComponent = ref playerMovePool.Get(playerEntity);
            playerMoveComponent.speed = PlayerData.Instance.speed;
            
            var playerAnimPool = ecsWorld.GetPool<AnimatedCharacterComponent>();
            playerAnimPool.Add(playerEntity);
            ref var playerAnimComponent = ref playerAnimPool.Get(playerEntity);

            var playerComponentPool = ecsWorld.GetPool<PlayerComponent>();
            playerComponentPool.Add(playerEntity);
            ref var playerComponent = ref playerComponentPool.Get(playerEntity);
            
            var spawnedPrefab = GameObject.Instantiate(PlayerData.Instance.prefab, PlayerData.Instance.spawnPoint,
                Quaternion.identity);
            spawnedPrefab.GetComponent<HitCheckerView>().ecsWorld = ecsWorld;
            
            playerComponent.rotateSpeed = PlayerData.Instance.rotateSpeed;
            playerComponent.collider = spawnedPrefab.GetComponent<CapsuleCollider>();
            playerComponent.rb = spawnedPrefab.GetComponent<Rigidbody>();
            playerComponent.playerTransform = spawnedPrefab.transform;
            
            playerAnimComponent.animator = spawnedPrefab.GetComponent<Animator>();
            playerInputComponent.clickGroundMask = PlayerData.Instance.clickGroundMask;
        }
    }
}