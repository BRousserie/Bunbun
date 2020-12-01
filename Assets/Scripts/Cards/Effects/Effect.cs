using System;
using UnityEngine;

public struct PlayEffectData
{
    public Character source;
    public Character target;

    public PlayEffectData(Character _source, Character _target)
    {
        source = _source;
        target = _target;
    }
}

public abstract class Effect : ScriptableObject
{
    public int value;
    public int remainingTurns;

    public Effect(int _value = 0, int _remainingTurns = 0)
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
    public abstract void Repeat(Character target);
    public abstract string Description();
    
    public bool DecrementRemainingTurns()
    {
        return (remainingTurns == -1) ? false : --remainingTurns == 0; // True if the effect is exhausted
    }
    
    public virtual void Remove()
    {
        throw new NotImplementedException();
    }
}