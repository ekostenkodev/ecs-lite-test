using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core
{
    public class InjectSystem : IEcsInitSystem
    {
        private readonly DiContainer _container;

        public InjectSystem(DiContainer container)
        {
            _container = container;
        }

        public void Init(EcsSystems systems)
        {
            _container.Bind<EcsWorld>().FromInstance(systems.GetWorld()).AsSingle();
            
            InjectToAllSystems(systems);
        }

        private void InjectToAllSystems(EcsSystems systems)
        {
            IEcsSystem[] list = null;
            var count = systems.GetAllSystems(ref list);
            for (var i = 0; i < count; i++)
            {
                _container.Inject(list[i]);
            }
        }
    }
}