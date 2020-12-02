using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class CardManager : Singleton<CardManager>
{
    public List<Card> AllPlayerCards { get; private set; }
    public List<Rarity> Rarities { get; private set; }
    private float DropRatesSum;
    
    // Start is called before the first frame update
    void Start()
    {
        AllPlayerCards = Resources.LoadAll("ScriptableObjects/Cards/Players", typeof(Card)).Cast<Card>().ToList();
        Rarities = Resources.LoadAll("ScriptableObjects/Rarities", typeof(Rarity))
            .Cast<Rarity>().OrderByDescending(r => r.DropRate).ToList();
        DropRatesSum = Rarities.Sum(r => r.DropRate);
    }

    public Card GetRandomCard(CardType type = CardType.Default, Rarity rarity = null, bool andRarer = true)
    {
        List<Card> desirableCards = AllPlayerCards;
        
        if (type != CardType.Default)
            desirableCards = desirableCards.Where(c => c.CardType == type).ToList();

        if (andRarer)
            rarity = GetRandomRarity((rarity != null) ? Rarities.IndexOf(rarity) : 0);
        
        return desirableCards.Where(c => c.Rarity == rarity).ToList().Random();
    }

    private Rarity GetRandomRarity(int leastRarityIndex)
    {
        float sum = 0;
        float lowerBound = 0;
        float pick = DropRatesSum;
        
        for (int r = 0; r < Rarities.Count; r++)
        {
            sum += Rarities[r].DropRate;
            if (r < leastRarityIndex) lowerBound += Rarities[r].DropRate;
            else if (r == leastRarityIndex) pick = Random.Range(lowerBound, DropRatesSum);

            if (sum > pick) return Rarities[r];
        }
        Debug.LogError("GetRandomRarity didn't return during for-loop");
        return Rarities[0];
    }
    
    // TODO : LE JOUEUR CHOISIT QUEL TYPE DE CARTE IL LOOT
}
