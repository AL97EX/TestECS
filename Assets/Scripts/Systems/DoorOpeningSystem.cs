using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class DoorOpeningSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var doorFilter = systems.GetWorld().Filter<DoorComponent>().End();
            var hitFilter = systems.GetWorld().Filter<HitComponent>().End();
            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var doorPool = systems.GetWorld().GetPool<DoorComponent>();
            var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
            
            foreach (var hitObject in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(hitObject);
                ref var buttonComponent = ref buttonPool.Get(hitObject);

                foreach (var doorObject in doorFilter)
                {
                    ref var doorComponent = ref doorPool.Get(doorObject);
                    
                    if (buttonComponent.isClicked && hitComponent.hittedObjectTag == doorComponent.doorTag)
                    {
                        if (doorComponent.doorTransform.position == doorComponent.openedPosition) return;
                        
                        doorComponent.doorTransform.position =
                            Vector3.MoveTowards(doorComponent.doorTransform.position, doorComponent.openedPosition,
                                Time.deltaTime * doorComponent.speedOpening);
                    }
                }
            }
        }
    }
}