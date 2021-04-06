using Characters;
using UnityEngine;

namespace Cards.Effects
{
    [CreateAssetMenu(fileName = "DrawEffect", menuName = "Cards/Effects/Draw")]
    public class DrawEffect : Effect
    {
        public DrawEffect(Targets _target, int _value = 0, int _remainingTurns = 0) 
            : base(_target, _value, _remainingTurns)
        {
        }

        protected override void Apply(PlayEffectData data)
        {
            if (remainingTurns == 0)
                data.Target.Deck.Draw(value);
            else
                data.Target.TurnEndsEffects.Add(this);
        }

        public override void Repeat(Character target)
        {
            target.Deck.Draw(value);
        }

        public override string Description()
        {
            string description = "Draw " + value + " cards";
            if (remainingTurns > 0)
                description += " for " + remainingTurns + " turns";
            return description;
        }
    }
}
