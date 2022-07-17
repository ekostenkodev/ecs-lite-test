using Kadoy.DoorTest.Core.Scene;
using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] 
        private SceneData _sceneData;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneData>().FromInstance(_sceneData).AsSingle().NonLazy();
            Container.BindInterfacesTo<CoreStartup>().AsSingle().NonLazy();
        }
    }
}