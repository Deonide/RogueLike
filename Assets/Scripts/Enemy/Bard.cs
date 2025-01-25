using UnityEngine;

public class Bard : EnemyBase
{
    protected override void UseAbilityOne()
    {
        StrengthIncrease();
    }

    protected override void UseAbilityTwo()
    {
        SpikeTrap();
    }

    protected override void UseAbilityThree()
    {
       LightAttack();
    }
}
