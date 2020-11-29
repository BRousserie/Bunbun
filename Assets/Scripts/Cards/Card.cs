using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common, Rare, Epic, Legendary
}

public enum CardType
{
    Default, Attack, Effect, Power
}

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string Name;
    public Rarity Rarity;
    public CardType CardType;
    public List<Effect> Effects;

    public void Play(PlayEffectData data)
    {
        foreach (Effect effect in Effects)
        {
            effect.Play(data);
        }
    }
}
