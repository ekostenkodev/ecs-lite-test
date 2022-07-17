using Kadoy.DoorTest.Core.Movement;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Interact
{
    public class DoorOpenSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }

        public void Run(EcsSystems systems)
        {
            OpenDoors();
            StopDoors();
        }

        private void OpenDoors()
        {
            var doorsFilter = _world
                .Filter<DoorComponent>()
                .Inc<InteractActiveComponent>()
                .Inc<PositionComponent>()
                .Exc<InteractCompleteComponent>()
                .End();

            foreach (var entity in doorsFilter)
            {
                var door = _world.GetPool<DoorComponent>().Get(entity);
                var position = _world.GetPool<PositionComponent>().Get(entity);

                if (false == _world.GetPool<MoveToComponent>().Has(entity))
                {
                    _world.GetPool<MoveToComponent>().Add(entity);
                }
                
                ref var moveTo = ref _world.GetPool<MoveToComponent>().Get(entity);
                var isOpened = (moveTo.Value - position.Value).sqrMagnitude <= Mathf.Epsilon;
                
                if (isOpened)
                {
                    _world.GetPool<InteractCompleteComponent>().Add(entity);
                    
                    continue;
                }

                moveTo.Value = door.CloseStatePosition;
            }
        }
        
        private void StopDoors()
        {
            var doorsFilter = _world
                .Filter<DoorComponent>()
                .Inc<MoveToComponent>()
                .Exc<InteractActiveComponent>()
                .End();

            foreach (var entity in doorsFilter)
            {
                _world.GetPool<MoveToComponent>().Del(entity);
            }
        }
    }
}