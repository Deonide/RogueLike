using TMPro;
using UnityEngine;

public class Character : MonoBehaviour, IDamageble
{
    [Header("Health")]
    public int m_currentHealth = 1;
    public int m_maxHealth = 50;
    public int m_startingHealth = 50;
    public int m_armor;

    [Header("Debuffs")]
    public int m_poisonValue;
    public int m_weakValue;
    public int m_vulnerableValue;
    public bool m_isWeak;
    public bool m_isVulnerable;

    [Header("Buffs")]
    public int m_strength;
    public int m_utility;

    protected virtual void Update()
    {
        if (m_weakValue >= 1)
        {
            m_isWeak = true;
        }
        else
        {
            m_isWeak = false;
        }

        if (m_vulnerableValue >= 1)
        {
            m_isVulnerable = true;
        }
        else
        {
            m_isVulnerable = false;
        }
    }

    public virtual void Damage(int damage)
    {
        if (m_armor > 0)
        {
            damage -= m_armor;
        }    

        if(damage > 0)
        {
            m_currentHealth -= damage;
            Debug.Log("Health: " + m_currentHealth);
        }
    }


    public void DebuffsActivate()
    {
        if(m_poisonValue > 0)
        {
            Damage(m_poisonValue);
            m_poisonValue -= 1;
        }
        if(m_weakValue > 0)
        {
            m_weakValue -= 1;
        }
        if (m_vulnerableValue > 0)
        {
            m_vulnerableValue -= 1;
        }
    }
}
