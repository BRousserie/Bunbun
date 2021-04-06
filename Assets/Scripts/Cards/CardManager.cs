using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;


namespace Cards
{
    public class CardManager : Singleton<CardManager>
    {
        public List<Card> AllPlayerCards { get; private set; }
        private Sprite _attackSprite;
        private Sprite _skillSprite;
        private Sprite _powerSprite;
        public List<Rarity> Rarities { get; private set; }
        public List<CardType> Types { get; private set; }
        private float DropRatesSum;
    
        void Start()
        {
            AllPlayerCards = Resources.LoadAll("ScriptableObjects/Cards/Players", typeof(Card)).Cast<Card>().ToList();
            Rarities = Resources.LoadAll("ScriptableObjects/Rarities", typeof(Rarity))
                .Cast<Rarity>().OrderByDescending(r => r.DropRate).ToList();
            Types = Resources.LoadAll("ScriptableObjects/CardTypes", typeof(CardType)).Cast<CardType>().ToList();
            DropRatesSum = Rarities.Sum(r => r.DropRate);
        }

        public Card GetRandomCard(CardTypes type = CardTypes.Default, Rarity rarity = null, bool andRarer = true)
        {
            List<Card> desirableCards = AllPlayerCards;
        
            if (type != CardTypes.Default)
                desirableCards = desirableCards.Where(c => c.CardType == GetCardType(type)).ToList();

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

        public CardType GetCardType(CardTypes type)
        {
            return Types.Find(c => c.Type == type);
        }

        // TODO : LE JOUEUR CHOISIT QUEL TYPE DE CARTE IL LOOT
    }
}
