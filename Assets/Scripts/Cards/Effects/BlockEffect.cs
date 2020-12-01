using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "BlockEffect", menuName = "Effects/Block")]
public class BlockEffect : Effect
{

    protected override void Apply(PlayEffectData data)
    {
        if (remainingTurns == 0)
            data.target.AddBlock(value);
        else
            data.target.TurnEndsEffects.Add(this);
    }

    public override void Repeat(Character target)
    {
        target.AddBlock(value);
    }


    public override string Description()
    {
        string description = "Gain " + value + " block";
        if (remainingTurns > 0)
            description += " at the end for your turn for " + remainingTurns + " turns";
        if (remainingTurns == -1)
            description += " for " + remainingTurns + " turns";
        return description;
    }
}
