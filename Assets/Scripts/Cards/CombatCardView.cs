using System;

public class CombatCardView : CardView
{
    protected override void MouseUp()
    {
        if (CheckCardTarget())
            Card.Play(new PlayEffectData(owner, target));
        else
            ReturnToPosition();
    }

    private bool CheckCardTarget()
    {
        throw new NotImplementedException();
    }
}
