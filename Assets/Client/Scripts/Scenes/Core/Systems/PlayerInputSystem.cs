using Kadoy.DoorTest.Core.Input;
using Kadoy.DoorTest.Core.Scene;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Interact
{
    public class PlayerInputSystem :IEcsRunSystem
    {
        private SceneData _sceneData;
        private EcsWorld _world;
        
        [Inject]
        private void Construct(EcsWorld world, SceneData sceneData)
        {
            _world = world;
            _sceneData = sceneData;
        }

        public void Run(EcsSystems systems)
        {
            CheckMouseInput();
        }

        private void CheckMouseInput()
        {
            if (false == UnityEngine.Input.GetMouseButtonDown(0))
            {
                return;
            }

            var mousePosition = UnityEngine.Input.mousePosition;
            var ray = _sceneData.MainCamera.ScreenPointToRay(mousePosition);
            
            if (false == Physics.Raycast(ray, out var hit, LayerMasks.Floor))
            {
                return;
            }

            var entity = _world.NewEntity();
            ref var mouseInputEvent = ref _world.GetPool<InputMouseComponent>().Add(entity);
            
            mouseInputEvent.Position = hit.point;
        }
    }
}