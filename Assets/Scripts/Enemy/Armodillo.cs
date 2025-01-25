using UnityEngine;

public class Armodillo : EnemyBase
{
    protected override void UseAbilityOne()
    {
        SpikeTrap();
    }

    protected override void UseAbilityTwo()
    {
        LightAttack();
    }

    protected override void UseAbilityThree()
    {
        WeakeningStrike();
    }
}
