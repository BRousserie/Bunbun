using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AttackEffect : Effect
{
    public AttackEffect(int _value) : base(_value) { }

    protected override void Apply(PlayEffectData data)
    {
        data.target.TakeDamage(value);
    }
}
