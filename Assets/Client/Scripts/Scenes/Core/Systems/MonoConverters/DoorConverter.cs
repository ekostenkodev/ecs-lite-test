using Kadoy.DoorTest.Core.Interact;
using Kadoy.DoorTest.Core.Movement;
using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Kadoy.DoorTest.Core
{
    public class DoorConverter : IEcsMonoConverter
    {
        public void Convert(EcsWorld world)
        {
            var doors = Object.FindObjectsOfType<DoorView>();

            foreach (var door in doors)
            {
                CreateEntity(world, door);
            }
        }

        private static void CreateEntity(EcsWorld world, DoorView view)
        {
            var entity = world.NewEntity();

            world.GetPool<PositionComponent>().Add(entity).Value = view.Root.position;
            world.GetPool<TransformComponent>().Add(entity).Value = view.Root;
            world.GetPool<MovableComponent>().Add(entity).Speed = view.OpenSpeed;
            world.GetPool<DoorComponent>().Add(entity).CloseStatePosition = view.CloseStatePosition;
            world.GetPool<InteractiveObjectComponent>().Add(entity).Id = view.Id;
        }
    }
}