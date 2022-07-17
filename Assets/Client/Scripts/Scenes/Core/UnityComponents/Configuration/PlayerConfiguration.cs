using System;
using Kadoy.DoorTest.Core.View;
using UnityEngine;

namespace Kadoy.DoorTest.Core.Scene
{
    [Serializable]
    public class PlayerConfiguration
    {
        [field:SerializeField]
        public ActorView ActorPrefab { get; private set; }
        
        [field:SerializeField]
        public float MovementSpeed { get; private set; }
        
        [field:SerializeField]
        public float RotationSpeed { get; private set; }
    }
}