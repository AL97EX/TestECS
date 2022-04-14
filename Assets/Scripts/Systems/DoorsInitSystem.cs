using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class DoorsInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();

            for (int i = 0; i < gameData.doors.Length; i++)
            {
                var doorEntity = ecsWorld.NewEntity();
                var doorComponentPool = ecsWorld.GetPool<DoorComponent>();
                doorComponentPool.Add(doorEntity);
                ref var doorComponent = ref doorComponentPool.Get(doorEntity);
                doorComponent.doorTag = gameData.doors[i].doorTag;
                doorComponent.doorTransform = gameData.doors[i].doorTransform;
                doorComponent.openedPosition = gameData.doors[i].openedPosition;
                doorComponent.speedOpening = gameData.doors[i].speedOpening;
            }
        }
    }
}