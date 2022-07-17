using Kadoy.DoorTest.Core.Movement;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core.View
{
    public class TransformUpdateSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }
        
        public void Run(EcsSystems systems)
        {
            ApplyPosition();
            ApplyRotation();
        }

        private void ApplyPosition()
        {
            var transformFilter = _world
                .Filter<TransformComponent>()
                .Inc<PositionComponent>()
                .End();

            foreach (var entity in transformFilter)
            {
                var transform = _world.GetPool<TransformComponent>().Get(entity);
                var position = _world.GetPool<PositionComponent>().Get(entity);

                transform.Value.position = position.Value;
            }
        }
        
        private void ApplyRotation()
        {
            var transformFilter = _world
                .Filter<TransformComponent>()
                .Inc<RotationComponent>()
                .End();
            
            foreach (var entity in transformFilter)
            {
                var transform = _world.GetPool<TransformComponent>().Get(entity);
                var rotation = _world.GetPool<RotationComponent>().Get(entity);

                transform.Value.rotation = rotation.Value;
            }
        }
    }
}