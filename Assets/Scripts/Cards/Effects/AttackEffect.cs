using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffect", menuName = "Cards/Effects/Attack")]
public class AttackEffect : Effect
{
    public AttackEffect(Targets _target, int _value = 0, int _remainingTurns = 0) 
        : base(_target, _value, _remainingTurns)
    {
    }

    protected override void Apply(PlayEffectData data)
    {
        data.Target.TakeDamage(value);
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
        if (target == Targets.Ennemies)
            description += " to all ennemies";
        return description;
    }
}
