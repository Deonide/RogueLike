using UnityEngine;

public class Snake : EnemyBase
{
    protected override void UseAbilityOne()
    {
        PoisononousGas();
    }

    protected override void UseAbilityTwo()
    {
        LightAttack();
    }

    protected override void UseAbilityThree()
    {
        StrengthIncrease();
    }
}
