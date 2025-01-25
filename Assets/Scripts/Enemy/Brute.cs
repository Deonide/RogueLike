using UnityEngine;

public class Brute : EnemyBase
{
    protected override void UseAbilityOne()
    {
        VulnerableStrike();
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
