using System;
using OneLine;
using UnityEngine;
using Utils;

namespace Map
{
    [Serializable]
    public class MapLayer
    {
        [OneLineWithHeader] public BoundedFloat distanceFromPreviousLayer;

        [Tooltip("Distance between the nodes on this layer")]
        public float nodesApartDistance;

        [Tooltip("The higher the more random (0 makes nodes aligned)")]
        [Range(0f, 1f)] public float positionRandomRate;

        [Tooltip("Default room type for this map layer")]
        public RoomType roomType;

        [Tooltip("Chance to get a random different room type from the default one for this layer")]
        [Range(0f, 1f)] public float roomTypeRandomRate;
    }
}