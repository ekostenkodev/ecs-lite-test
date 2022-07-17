using UnityEngine;

namespace Kadoy.DoorTest.Core.View
{
    public interface IActorAnimationController
    {
        void SetMoveActive(bool active);
    }

    public class ActorAnimationController : IActorAnimationController
    {
        private static readonly int MoveKey = Animator.StringToHash("IsMove");

        private readonly Animator _animator;

        public ActorAnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void SetMoveActive(bool active)
        {
            _animator.SetBool(MoveKey, active);
        }
    }
}