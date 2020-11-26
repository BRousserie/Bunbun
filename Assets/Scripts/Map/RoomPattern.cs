using UnityEngine;

namespace Map
{
    public enum RoomType
    {
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Treasure,
        Store,
        Boss,
        Mystery
    }

    public class RoomPattern : ScriptableObject
    {
        public Sprite sprite;
        public RoomType type;
    }
}