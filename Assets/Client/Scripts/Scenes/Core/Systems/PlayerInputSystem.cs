using Kadoy.DoorTest.Core.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Kadoy.DoorTest.Core.Interact
{
    public class PlayerInputSystem :IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            CheckMouseInput(systems.GetWorld());
        }

        private void CheckMouseInput(EcsWorld world)
        {
            if (false == UnityEngine.Input.GetMouseButtonDown(0))
            {
                return;
            }

            var mousePosition = UnityEngine.Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            
            if (false == Physics.Raycast(ray, out var hit, ~0))
            {
                return;
            }

            var entity = world.NewEntity();
            ref var mouseInputEvent = ref world.GetPool<InputMouseComponent>().Add(entity);
            
            mouseInputEvent.Position = hit.point;
        }
    }
}