using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(menuName = "Cards/Rarity")]
    public class Rarity : ScriptableObject
    {
        public string Name;
        public float DropRate;
        public Color Color;
    }
}