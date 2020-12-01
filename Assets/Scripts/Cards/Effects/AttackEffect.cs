using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffect", menuName = "Effects/Attack")]
public class AttackEffect : Effect
{
    public bool AoE;

    public AttackEffect(int _value, bool _AoE = false) : base(_value)
    {
        AoE = _AoE;
    }

    protected override void Apply(PlayEffectData data)
    {
        data.target.TakeDamage(value);
    }

    public override void Repeat(Character target)
    {
        target.TakeDamage(value);
    }

    public override string Description()
    {
        string description = "Deal " + value + " damage";
        if (remainingTurns > 0)
            description += " for " + remainingTurns + " turns";
        if (AoE)
            description += " to all ennemies";
        return description;
    }
}
