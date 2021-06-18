using System;
using System.Collections.Generic;
using Cards.Effects;
using UnityEngine;

namespace Cards
{
    [Serializable, CreateAssetMenu(menuName = "Cards/Card")]
    public class Card : ScriptableObject
    {
        public string Name;
        [SerializeField] private string description;
        public string Description
        {
            get
            {
                if (description.Length == 0)
                    foreach (Effect effect in Effects) description += effect.Description() + "\n";
                return description;
            }
            private set { description = value; }
        }

        public Rarity Rarity;
        public CardType CardType;
        public List<Effect> Effects;
        public int EnergyCost;
        public Sprite Sprite;

        public void Play(PlayEffectData data)
        {
            foreach (Effect effect in Effects)
            {
                effect.Play(data);
            }
        }
    }
}
