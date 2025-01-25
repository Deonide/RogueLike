using ScriptableCard;
using UnityEngine;

public class Slime : EnemyBase
{
    protected override void UseAbilityOne()
    {
        LightAttack();
    }

    protected override void UseAbilityTwo()
    {
        WeakeningStrike();
    }

    protected override void UseAbilityThree()
    {
        HeavyAttack();
    }
}
