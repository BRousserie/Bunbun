using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypes
{
    Default, Attack, Skill, Power
}

[CreateAssetMenu(menuName = "Cards/Type")]
public class CardType : ScriptableObject
{
    public CardTypes Type;
    public Sprite Background;
}