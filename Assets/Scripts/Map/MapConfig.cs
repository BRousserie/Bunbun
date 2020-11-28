using System;
using System.Collections.Generic;
using Malee.List;
using OneLine;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        [Reorderable] public ListOfMapLayers layers;
        
        [OneLineWithHeader] public BoundedInt numOfPreBossNodes;
        [OneLineWithHeader] public BoundedInt numOfStartingNodes;

        public List<RoomPattern> roomPatterns;
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

        [Serializable]
        public class ListOfMapLayers : ReorderableArray<MapLayer>
        {
        }
    }
}