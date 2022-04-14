using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class WinSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var gameData = systems.GetShared<GameData>();
            var hitFilter = systems.GetWorld().Filter<HitComponent>().End();
            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var playerFilter = systems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = systems.GetWorld().GetPool<PlayerComponent>();
            
            foreach (var hitObject in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(hitObject);
                
                if (hitComponent.hittedObjectTag == Constants.WIN_TAG)
                {
                    foreach (var player in playerFilter)
                    {
                        ref var playerComponent = ref playerPool.Get(player);
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        gameData.winPanel.SetActive(true);
                        systems.GetWorld().DelEntity(player);
                        systems.GetWorld().DelEntity(hitObject);
                    }
                }
            }
        }
    }
}