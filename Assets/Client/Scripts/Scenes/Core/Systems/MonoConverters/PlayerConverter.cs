using Kadoy.DoorTest.Core.Interact;
using Kadoy.DoorTest.Core.Movement;
using Kadoy.DoorTest.Core.Scene;
using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Kadoy.DoorTest.Core
{
    public class PlayerConverter : IEcsMonoConverter
    {
        private readonly SceneData _sceneData;
        private readonly Configuration _configuration;
        
        public PlayerConverter(SceneData sceneData, Configuration configuration)
        {
            _sceneData = sceneData;
            _configuration = configuration;
        }
        
        public void Convert(EcsWorld world)
        {
            var entity = world.NewEntity();
            var view = Object.Instantiate(_configuration.Player.ActorPrefab, _sceneData.WorldRoot);

            world.GetPool<TransformComponent>().Add(entity).Value = view.Root;
            world.GetPool<ActorAnimationComponent>().Add(entity).Controller = new ActorAnimationController(view.Animator);
            world.GetPool<PositionComponent>().Add(entity).Value = _sceneData.PlayerSpawnPoint.position;
            world.GetPool<RotationComponent>().Add(entity).Value = Quaternion.identity;
            world.GetPool<RadiusComponent>().Add(entity).Value = view.Radius;
            world.GetPool<MovableComponent>().Add(entity).Speed = _configuration.Player.MovementSpeed;
            world.GetPool<RotatableComponent>().Add(entity).Speed = _configuration.Player.RotationSpeed;
            world.GetPool<InteractAbilityComponent>().Add(entity);
            world.GetPool<PlayerInputListenerComponent>().Add(entity);
            world.GetPool<RadiusComponent>();
        }
    }
}