using Kadoy.DoorTest.Core.Input;
using Kadoy.DoorTest.Core.Movement;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core.Interact
{
    public class InputApplySystem : IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorld world)
        {
            _world = world;
        }

        public void Run(EcsSystems systems)
        {
            ApplyMouseInput();
        }

        private void ApplyMouseInput()
        {
            var mouseInputFilter = _world.Filter<InputMouseComponent>().End();
            var playerFilter = _world
                .Filter<PlayerInputListenerComponent>()
                .End();

            foreach (var inputEntity in mouseInputFilter)
            {
                var input = _world.GetPool<InputMouseComponent>().Get(inputEntity);
                
                foreach (var playerEntity in playerFilter)
                {
                    if (false == _world.GetPool<MoveToComponent>().Has(playerEntity))
                    {
                        _world.GetPool<MoveToComponent>().Add(playerEntity);
                    }
                    
                    _world.GetPool<MoveToComponent>().Get(playerEntity).Value = input.Position;
                }

                _world.GetPool<InputMouseComponent>().Del(inputEntity);
            }
        }
    }
}