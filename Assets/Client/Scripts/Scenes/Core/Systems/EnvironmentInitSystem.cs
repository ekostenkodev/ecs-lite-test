using Kadoy.DoorTest.Core.Scene;
using Leopotam.EcsLite;
using Zenject;

namespace Kadoy.DoorTest.Core
{
    public class EnvironmentInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SceneData _sceneData;
        private Configuration _configuration;

        [Inject]
        private void Construct(EcsWorld world, SceneData sceneData, Configuration configuration)
        {
            _world = world;
            _sceneData = sceneData;
            _configuration = configuration;
        }
        
        public void Init(EcsSystems systems)
        {
            var converters = new IEcsMonoConverter[]
            {
                new PlayerConverter(_sceneData, _configuration),
                new DoorConverter(),
                new ButtonConverter()
            };

            foreach (var converter in converters)
            {
                converter.Convert(_world);
            }
        }
    }
}