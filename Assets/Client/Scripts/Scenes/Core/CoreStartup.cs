using Kadoy.DoorTest.Core.Interact;
using Kadoy.DoorTest.Core.Movement;
using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Kadoy.DoorTest.Core
{
    public class CoreStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
    
        public void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);

            _updateSystems 
                // client
                .Add(new PlayerInputSystem())
                .Add(new TransformUpdateSystem())
                
                // server
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Init();
        }

        public void Update()
        {
            _updateSystems?.Run();
        }

        public void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}