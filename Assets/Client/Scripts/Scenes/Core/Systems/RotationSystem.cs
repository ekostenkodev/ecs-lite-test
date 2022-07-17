using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Movement
{
    public class RotationSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            ApplyRotation(systems.GetWorld());
        }

        private void ApplyRotation(EcsWorld world)
        {
            var deltaTime = Time.deltaTime;
            var movableFilter = world
                .Filter<RotatableComponent>()
                .Inc<RotationComponent>()
                .Inc<PositionComponent>()
                .Inc<MoveToComponent>()
                .End();
            
            foreach (var entity in movableFilter)
            {
                ref var currentRotation = ref world.GetPool<RotationComponent>().Get(entity);
                var currentPosition = world.GetPool<PositionComponent>().Get(entity);
                var moveTo = world.GetPool<MoveToComponent>().Get(entity);
                var rotatable = world.GetPool<RotatableComponent>().Get(entity);
                
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