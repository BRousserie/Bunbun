using System;
using UnityEngine;

public struct PlayEffectData
{
    public Character Source;
    public Character Target;

    public PlayEffectData(Character _source, Character _target)
    {
        Source = _source;
        Target = _target;
    }
}

public enum Targets
{
    EffectDependant, // Different effects on this card have different targets
    Self, // Towards card owner
    Team, // Towards player team
    Player, // Towards a player
    Ennemy, // Towards an ennemy
    Ennemies // Towards all ennemies
}

public abstract class Effect : ScriptableObject
{
    public Targets target;
    public int value;
    public int remainingTurns;

    public Effect(Targets _target, int _value = 0, int _remainingTurns = 0)
    {
        target = _target;
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