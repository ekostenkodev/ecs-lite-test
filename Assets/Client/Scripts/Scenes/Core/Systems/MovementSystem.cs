using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Movement
{
    public class MovementSystem : IEcsRunSystem
    {
        private const float ArriveProximity = 0.01f;
        
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }

        public void Run(EcsSystems systems)
        {
            ApplyMoveTo();
        }

        private void ApplyMoveTo()
        {
            var deltaTime = Time.deltaTime;
            var movableFilter = _world
                .Filter<MovableComponent>()
                .Inc<PositionComponent>()
                .Inc<MoveToComponent>()
                .End();
            
            foreach (var entity in movableFilter)
            {
                ref var currentPosition = ref _world.GetPool<PositionComponent>().Get(entity);
                var moveTo = _world.GetPool<MoveToComponent>().Get(entity);
                var movable = _world.GetPool<MovableComponent>().Get(entity);
                
                var distance = moveTo.Value - currentPosition.Value;
                var isReached = distance.sqrMagnitude <= ArriveProximity;

                if (isReached)
                {
                    _world.GetPool<MoveToComponent>().Del(entity);
                    
                    continue;
                }

                currentPosition.Value += distance.normalized * (movable.Speed * deltaTime);
            }
        }
    }
}
