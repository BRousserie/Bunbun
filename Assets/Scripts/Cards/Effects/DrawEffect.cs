using System;
using System.Collections;
using System.Collections.Generic;
using Malee.List;
using UnityEditor.SceneManagement;
using UnityEngine;


[CreateAssetMenu(fileName = "DrawEffect", menuName = "Effects/Draw")]
public class DrawEffect : Effect
{
    
    protected override void Apply(PlayEffectData data)
    {
        if (remainingTurns == 0)
            data.target.Deck.Draw(value);
        else
            data.target.TurnEndsEffects.Add(this);
    }

    public override void Repeat(Character target)
    {
        target.Deck.Draw(value);
    }

    public override string Description()
    {
        string description = "Draw " + value + " cards";
        if (remainingTurns > 0)
            description += " for " + remainingTurns + " turns";
        return description;
    }
}
