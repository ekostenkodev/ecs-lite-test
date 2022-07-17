using UnityEngine;

namespace Kadoy.DoorTest.Core
{
    public static class LayerMasks
    {
        private const string FloorKey = "Floor";

        public static int Floor { get; }

        static LayerMasks()
        {
            Floor = LayerMask.GetMask(FloorKey);
        }
    }
}