using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BlockEffect : Effect
{

    protected override void Apply(PlayEffectData data)
    {
        data.target.AddBlock(value);
    }
    
}
