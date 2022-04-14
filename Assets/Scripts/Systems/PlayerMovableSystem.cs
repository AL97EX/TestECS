using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerMovableSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<PlayerComponent>().End();
            var playerComponentPool = systems.GetWorld().GetPool<PlayerComponent>();
            var playerMoveComponentPool = systems.GetWorld().GetPool<PlayerMoveComponent>();
            var playerInputComponentPool = systems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerComponentPool.Get(entity);
                ref var playerMoveComponent = ref playerMoveComponentPool.Get(entity);
                ref var playerInputComponent = ref playerInputComponentPool.Get(entity);

                bool isPlayerOnPlace = 
                    playerComponent.playerTransform.position == playerComponent.destinationPosition;
                
                playerMoveComponent.isMoving = !isPlayerOnPlace
                    ? true
                    : false;

                if (playerMoveComponent.isMoving)
                    playerComponent.playerTransform.position =
                        Vector3.MoveTowards(playerComponent.playerTransform.position,
                            playerComponent.destinationPosition, Time.deltaTime * playerMoveComponent.speed);
            }
        }
    }
}