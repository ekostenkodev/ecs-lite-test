using Kadoy.DoorTest.Core.Interact;
using Kadoy.DoorTest.Core.Movement;
using Kadoy.DoorTest.Core.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Kadoy.DoorTest.Core
{
    public class ButtonConverter : IEcsMonoConverter
    {
        public void Convert(EcsWorld world)
        {
            ConvertCircleButtons(world);
        }

        private void ConvertCircleButtons(EcsWorld world)
        {
            var buttons = Object.FindObjectsOfType<CircleButtonView>();

            foreach (var button in buttons)
            {
                CreateEntity(world, button);
            }
        }

        private void CreateEntity(EcsWorld world, CircleButtonView view)
        {
            var entity = world.NewEntity();

            world.GetPool<PositionComponent>().Add(entity).Value = view.Root.position;
            world.GetPool<RadiusComponent>().Add(entity).Value = view.InteractRadius;
            world.GetPool<InteractableComponent>().Add(entity).InteractiveEntity = GetCompatibleInteract(world, view.InteractiveObjectId);
        }

        private EcsPackedEntity GetCompatibleInteract(EcsWorld world, string id)
        {
            var interactFilter = world.Filter<InteractiveObjectComponent>().End();

            foreach (var entity in interactFilter)
            {
                if (world.GetPool<InteractiveObjectComponent>().Get(entity).Id == id)
                {
                    return world.PackEntity(entity);
                }
            }

            return default;
        }
    }
}