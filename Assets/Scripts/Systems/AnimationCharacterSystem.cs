using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class AnimationCharacterSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var playerFilter = systems.GetWorld().Filter<PlayerComponent>().End();
            var animatedCharacterPool = systems.GetWorld().GetPool<AnimatedCharacterComponent>();
            var moveCharacterPool = systems.GetWorld().GetPool<PlayerMoveComponent>();

            foreach (var player in playerFilter)
            {
                ref var animatedCharacterComponent = ref animatedCharacterPool.Get(player);
                ref var moveCharacterComponent = ref moveCharacterPool.Get(player);

                animatedCharacterComponent.animator.SetBool(Constants.ANIM_RUNNING_TAG, moveCharacterComponent.isMoving);
            }
        }
    }
}