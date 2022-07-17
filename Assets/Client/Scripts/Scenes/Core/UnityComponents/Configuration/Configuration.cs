using UnityEngine;
using Zenject;

namespace Kadoy.DoorTest.Core.Scene
{
    [CreateAssetMenu(menuName = "Core/Configuration", fileName = "Configuration")]
    public class Configuration : ScriptableObjectInstaller<Configuration>
    {
        public PlayerConfiguration Player;

        public override void InstallBindings()
        {
            Container.Bind<Configuration>().FromInstance(this).AsSingle().NonLazy();
        }
    }
}