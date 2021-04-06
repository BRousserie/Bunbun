using Characters;
using UnityEngine;

namespace Cards.Effects
{
    [CreateAssetMenu(fileName = "BlockEffect", menuName = "Cards/Effects/Block")]
    public class BlockEffect : Effect
    {
        public BlockEffect(Targets _target, int _value = 0, int _remainingTurns = 0) 
            : base(_target, _value, _remainingTurns)
        {
        }

        protected override void Apply(PlayEffectData data)
        {
            if (remainingTurns == 0)
                data.Target.AddBlock(value);
            else
                data.Target.TurnEndsEffects.Add(this);
        }

        public override void Repeat(Character target)
        {
            target.AddBlock(value);
        }


        public override string Description()
        {
            string description = "Gain " + value + " block";
            if (remainingTurns > 0)
                description += " at the end for your turn for " + remainingTurns + " turns";
            if (remainingTurns == -1)
                description += " at the end of your turns";
            return description;
        }
    }
}
