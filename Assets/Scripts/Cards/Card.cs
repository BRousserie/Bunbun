using System;
using System.Collections.Generic;
using Malee.List;
using Map;
using UnityEngine;

public enum CardType
{
    Default, Attack, Skill, Power
}

[Serializable, CreateAssetMenu]
public class Card : ScriptableObject
{
    public string Name;
    private string description;
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
