using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected int m_currentHealth = 1;

    protected int m_armor;

    public int m_weakValue;
    public int m_vulnerableValue;
    public bool m_isWeak;
    public bool m_isVulnerable;

    public int m_poisonValue;
    public int m_strength;
    public int m_utility;


    void Update()
    {
        if (m_weakValue > 0)
        {
            m_isWeak = true;

        }
        else
        {
            m_isWeak = false;
        }

        if (m_vulnerableValue > 0)
        {
            m_isVulnerable = true;
        }
        else
        {
            m_isVulnerable = false;
        }
    }

    public int Damage(int damage)
    {
        if (m_armor > 0)
        {
            damage -= m_armor;
            m_armor -= damage;
        }
        m_currentHealth -= damage;
/*        healthChangedEvent.Invoke(m_currentHealth);*/
        Debug.Log("Health: " + m_currentHealth);
        return damage;
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
