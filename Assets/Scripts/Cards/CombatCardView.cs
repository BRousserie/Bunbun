using System;
using Cards.Effects;

namespace Cards
{
    public class CombatCardView : CardView
    {
        protected override void MouseUp()
        {
            if (CheckCardTarget())
                Card.Play(new PlayEffectData(owner, target));
            else
                ReturnToPosition();
        }

        protected override void Drag()
        {
            throw new NotImplementedException();
        }

        private bool CheckCardTarget()
        {
            throw new NotImplementedException();
        }
    }
}
