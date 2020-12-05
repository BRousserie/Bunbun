using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Rarity")]
public class Rarity : ScriptableObject
{
    public string Name;
    public float DropRate;
    public Color Color;
}