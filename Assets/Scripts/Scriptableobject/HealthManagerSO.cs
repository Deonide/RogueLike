
    using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "New Health Manager",
    menuName = "Base/New HealthManager",
    order = 1
)]
public class HealthSO : ScriptableObject
{

    public int m_maxHealth = 100;
    public int m_currentHealth = 100;
    public int m_armor;

    [System.NonSerialized]
    public UnityEvent<int> healthChangedEvent;
    
    public void OnEnable()
    {
        m_currentHealth = m_maxHealth;
        if (healthChangedEvent == null)
        {
            healthChangedEvent = new UnityEvent<int>();
        }
    }






    public void Heal(int amount)
    {
        m_currentHealth += amount;
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
        healthChangedEvent.Invoke(m_currentHealth);
    }

    public void SetMaxHealth(int amount)
    {
        m_maxHealth = amount;
    }

    public int GetCurrentHealth()
    {
        return m_currentHealth;
    }
}

