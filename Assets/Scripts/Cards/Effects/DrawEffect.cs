using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DrawEffect : Effect
{
    
    protected override void Apply(PlayEffectData data)
    {
        data.target.Deck.Draw(value);
    }
}
