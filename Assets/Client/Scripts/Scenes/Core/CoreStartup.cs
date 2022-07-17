using System;
using Kadoy.DoorTest.Core.Interact;
using Kadoy.DoorTest.Core.Movement;
using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core
{
    public class CoreStartup : IInitializable, ITickable, IDisposable
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
    
        public void Initialize()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);

            _updateSystems 
                // client
                .Add(new InjectSystem(_container))
                .Add(new EnvironmentInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new InputApplySystem())
                .Add(new ActorAnimationSystem())
                .Add(new TransformUpdateSystem())
                
                // server
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new InteractSystem())
                .Add(new DoorOpenSystem())
                .Init();
        }

        public void Tick()
        {
            _updateSystems?.Run();
        }

        public void Dispose()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}