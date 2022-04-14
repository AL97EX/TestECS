using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class ClickButtonSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var hitFilter = systems.GetWorld().Filter<HitComponent>().End();
            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();

            foreach (var hitObject in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(hitObject);
                ref var buttonComponent = ref buttonPool.Get(hitObject);

                if (buttonComponent.isClicked)
                {
                    Debug.Log("Logig button hit open door");
                    //buttonComponent.isClicked = false;
                }
            }
        }
    }
}