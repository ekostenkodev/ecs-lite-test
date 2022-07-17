using Kadoy.DoorTest.Core.Movement;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core.Interact
{
    public class InteractSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }

        public void Run(EcsSystems systems)
        {
            CheckRadiusInteract();
        }

        private void CheckRadiusInteract()
        {
            var interactAbilityFilter = _world
                .Filter<InteractAbilityComponent>()
                .Inc<PositionComponent>()
                .Inc<RadiusComponent>()
                .End();

            var interactableFilter = _world
                .Filter<InteractableComponent>()
                .Inc<PositionComponent>()
                .Inc<RadiusComponent>()
                .End();
            
            foreach (var abilityEntity in interactAbilityFilter)
            {
                foreach (var interactableEntity in interactableFilter)
                {
                    var interactive = _world.GetPool<InteractableComponent>().Get(interactableEntity).InteractiveEntity;
                    
                    if (interactive.Unpack(_world, out var interactiveEntity))
                    {
                        if (IsInRange(abilityEntity, interactableEntity))
                        {
                            if (false == _world.GetPool<InteractActiveComponent>().Has(interactableEntity))
                            {
                                _world.GetPool<InteractActiveComponent>().Add(interactableEntity);
                                _world.GetPool<InteractActiveComponent>().Add(interactiveEntity);
                            }
                        }
                        else
                        {
                            _world.GetPool<InteractActiveComponent>().Del(interactableEntity);
                            _world.GetPool<InteractActiveComponent>().Del(interactiveEntity);
                        }
                    }
                }
            }
        }

        private bool IsInRange(int abilityEntity, int interactableEntity)
        {
            var positionPool = _world.GetPool<PositionComponent>();
            var radiusPool = _world.GetPool<RadiusComponent>();

            var interactPosition = positionPool.Get(abilityEntity).Value;
            var interactRadius = radiusPool.Get(abilityEntity).Value;

            var interactablePosition = positionPool.Get(interactableEntity).Value;
            var interactableRadius = radiusPool.Get(interactableEntity).Value;

            var sqrDistance = (interactPosition - interactablePosition).sqrMagnitude;
            var radius = interactRadius + interactableRadius;

            var inRange = sqrDistance <= radius * radius;

            return inRange;
        }
    }
}