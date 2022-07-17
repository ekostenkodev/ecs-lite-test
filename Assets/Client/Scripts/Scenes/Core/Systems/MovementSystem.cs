using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Movement
{
    public class MovementSystem : IEcsRunSystem
    {
        private const float ArriveProximity = 0.01f;
        
        public void Run(EcsSystems systems)
        {
            ApplyMoveTo(systems.GetWorld());
        }

        private void ApplyMoveTo(EcsWorld world)
        {
            var deltaTime = Time.deltaTime;
            var movableFilter = world
                .Filter<MovableComponent>()
                .Inc<PositionComponent>()
                .Inc<MoveToComponent>()
                .End();
            
            foreach (var entity in movableFilter)
            {
                ref var currentPosition = ref world.GetPool<PositionComponent>().Get(entity);
                var moveTo = world.GetPool<MoveToComponent>().Get(entity);
                var movable = world.GetPool<MovableComponent>().Get(entity);
                
                var distance = moveTo.Value - currentPosition.Value;
                var isReached = distance.sqrMagnitude <= ArriveProximity;

                if (isReached)
                {
                    world.GetPool<MoveToComponent>().Del(entity);
                    
                    continue;
                }

                currentPosition.Value += distance.normalized * (movable.Speed * deltaTime);
            }
        }
    }
}
