using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core.Movement
{
    public class ActorAnimationSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }
        
        public void Run(EcsSystems systems)
        {
            ApplyMove();
        }
        
        private void ApplyMove()
        {
            var moveFilter = _world
                .Filter<ActorAnimationComponent>()
                .Inc<MovableComponent>()
                .End();

            foreach (var entity in moveFilter)
            {
                var animation = _world.GetPool<ActorAnimationComponent>().Get(entity);
                var isMoving = _world.GetPool<MoveToComponent>().Has(entity);

                animation.Controller.SetMoveActive(isMoving);
            }
        }
    }
}