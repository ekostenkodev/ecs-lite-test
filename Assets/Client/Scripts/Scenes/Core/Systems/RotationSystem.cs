using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Movement
{
    public class RotationSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }

        public void Run(EcsSystems systems)
        {
            ApplyRotation();
        }

        private void ApplyRotation()
        {
            var deltaTime = Time.deltaTime;
            var movableFilter = _world
                .Filter<RotatableComponent>()
                .Inc<RotationComponent>()
                .Inc<PositionComponent>()
                .Inc<MoveToComponent>()
                .End();
            
            foreach (var entity in movableFilter)
            {
                ref var currentRotation = ref _world.GetPool<RotationComponent>().Get(entity);
                var currentPosition = _world.GetPool<PositionComponent>().Get(entity);
                var moveTo = _world.GetPool<MoveToComponent>().Get(entity);
                var rotatable = _world.GetPool<RotatableComponent>().Get(entity);
                
                var targetDirection = (moveTo.Value - currentPosition.Value).normalized;
                var speed = rotatable.Speed * deltaTime;

                currentRotation.Value = CalculateRotationTo(currentRotation.Value, targetDirection, speed);
            }
        }
        
        private Quaternion CalculateRotationTo(Quaternion from, Vector3 to, float speed)
        {
            return Quaternion.RotateTowards(
                from,
                Quaternion.LookRotation(to),
                speed);
        }
    }
}