using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputComponentPool = systems.GetWorld().GetPool<PlayerInputComponent>();
            var playerComponentPool = systems.GetWorld().GetPool<PlayerComponent>();
            
            foreach (var input in filter)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ref var playerInput = ref playerInputComponentPool.Get(input);
                    ref var playerComponent = ref playerComponentPool.Get(input);
                    
                    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(myRay, out hitInfo, 1000, playerInput.clickGroundMask))
                    {
                        playerComponent.destinationPosition = hitInfo.point;
                    }
                }
            }
        }
    }
}