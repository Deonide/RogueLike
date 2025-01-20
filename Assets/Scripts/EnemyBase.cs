using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : Character
{
    [SerializeField]
    private WaveManager m_waveManager;
    private int m_abilityToUse;

    public int m_spikeValue;
    protected virtual void Update()
    {
        if(m_waveManager == null)
        {
            m_waveManager = FindFirstObjectByType<WaveManager>();
        }

        if(m_currentHealth <= 0)
        {
            m_waveManager.RemoveFromList(this.gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void AbilityToUse()
    {
        m_abilityToUse = Random.Range(0, 3);

        switch(m_abilityToUse)
        {
            case 0:
                UseAbilityOne();
                break;
            case 1:
                UseAbilityTwo();
                break;
            case 2:
                UseAbilityThree();
                break;
        }
    }

    public void IncreaseArmor(int armorIncrease)
    {
        m_armor += armorIncrease;
    }

    protected virtual void UseAbilityOne()
    {

    }

    protected virtual void UseAbilityTwo()
    {

    }

    protected virtual void UseAbilityThree()
    {

    }
}
