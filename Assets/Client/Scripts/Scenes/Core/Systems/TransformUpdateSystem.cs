using Kadoy.DoorTest.Core.Movement;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core.View
{
    public class TransformUpdateSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            ApplyPosition(systems.GetWorld());
            ApplyRotation(systems.GetWorld());
        }

        private void ApplyPosition(EcsWorld world)
        {
            var transformFilter = world
                .Filter<TransformComponent>()
                .Inc<PositionComponent>()
                .End();

            foreach (var entity in transformFilter)
            {
                var transform = world.GetPool<TransformComponent>().Get(entity);
                var position = world.GetPool<PositionComponent>().Get(entity);

                transform.Value.position = position.Value;
            }
        }
        
        private void ApplyRotation(EcsWorld world)
        {
            var transformFilter = world
                .Filter<TransformComponent>()
                .Inc<RotationComponent>()
                .End();
            
            foreach (var entity in transformFilter)
            {
                var transform = world.GetPool<TransformComponent>().Get(entity);
                var rotation = world.GetPool<RotationComponent>().Get(entity);

                transform.Value.rotation = rotation.Value;
            }
        }
    }
}