using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public struct PlayEffectData
{
    public Character source;
    public Character target;
}

public abstract class Effect
{
    public int value;
    public int remainingTurns;

    public Effect(int _value = 0, int _remainingTurns = -1)
    {
        value = _value;
        remainingTurns = _remainingTurns;
    }

    public void Play(PlayEffectData data)
    {
        if (remainingTurns != -1)
        {
            if (--remainingTurns == 0)
            {
                Remove();
            }
        }
        Apply(data);
    }

    protected abstract void Apply(PlayEffectData data);

    public virtual void Remove()
    {
        throw new NotImplementedException();
    }
}