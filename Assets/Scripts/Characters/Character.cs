using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    [HideInInspector] public List<Effect> TurnBeginsEffects;
    [HideInInspector] public List<Effect> TurnEndsEffects;
    [HideInInspector] public List<Effect> GivingDamageEffects;
    [HideInInspector] public List<Effect> TakingDamageEffects;
    public int MaxHP;
    public int HP;
    public int Block;
    public Deck Deck;

    // Stats-related methods
    public void RestoreFullHP() { HP = MaxHP; }
    public void RestoreHP(int amount) { HP = Mathf.Min(HP + amount, MaxHP); }
    public void AddBlock(int amount) { Block += amount; }
    public void RemoveBlock() { Block = 0; }

    public void TakeDamage(int amount)
    {
        int hpLost = amount - Block;
        Block = Mathf.Max(Block - amount, 0);
        HP = Mathf.Max(HP - hpLost, 0);
    }

    // Event methods
    public void OnTurnBegins()
    {
        foreach (Effect turnBeginEffect in TurnBeginsEffects)
        {
            turnBeginEffect.Repeat(this);
            turnBeginEffect.DecrementRemainingTurns();
        }
    }

    public void OnTurnEnds()
    {
        foreach (Effect turnEndsEffect in TurnEndsEffects)
        {
            turnEndsEffect.Repeat(this);
            turnEndsEffect.DecrementRemainingTurns();
        }
        foreach (Effect givingDamageEffect in GivingDamageEffects)
            givingDamageEffect.DecrementRemainingTurns();
        foreach (Effect turnEndEffect in TakingDamageEffects)
            turnEndEffect.DecrementRemainingTurns();
        foreach (Effect turnBeginEffect in TurnBeginsEffects)
            turnBeginEffect.DecrementRemainingTurns();
    }

    public int OnGivingDamage(int amount)
    {
        foreach (Effect givingDamageEffect in GivingDamageEffects)
        {
            givingDamageEffect.Repeat(this);
            givingDamageEffect.DecrementRemainingTurns();
        }
        
        return amount;
    }

    public int OnTakingDamage(int amount)
    {
        foreach (Effect turnEndEffect in TakingDamageEffects)
        {
            turnEndEffect.Repeat(this);
            turnEndEffect.DecrementRemainingTurns();
        }

        return amount;
    }
}
