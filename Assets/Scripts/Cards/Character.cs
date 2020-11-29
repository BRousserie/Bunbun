using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int MaxHP { get; private set; }
    public int HP { get; private set; }
    public int Block { get; set; }
    public Deck Deck { get; private set; }

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

    
}
