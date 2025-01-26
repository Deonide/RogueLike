using TMPro;
using UnityEngine;

public class Player : Character
{


    [Header("Energy")]
    public int m_currentEnergy;
    public int m_energyLevel = 3;

    [Header("Money")]
    public int m_money;
    private int m_startingMoney = 250;



    private DrawPileManager m_drawPileManager;
    private void Start()
    {
        m_money = m_startingMoney;
        m_currentHealth = m_maxHealth;
        m_drawPileManager = FindFirstObjectByType<DrawPileManager>();
        GameManager.Instance.m_uIManager.UpdateText();
    }

    #region Armor + Health
    public void IncreaseArmor(int armorIncrease)
    {
        m_armor += armorIncrease;
        GameManager.Instance.m_uIManager.UpdateText();
    }

    public void DecreaseArmor(int damage)
    {
        m_armor -= damage;
        GameManager.Instance.m_uIManager.UpdateText();
    }

    public void ResetArmor()
    {
        m_armor = 0;
    }

    public void IncreaseMaxHealth()
    {
        m_maxHealth += 10;
        m_currentHealth += 10;
        GameManager.Instance.m_uIManager.UpdateText();
    }
    #endregion

    protected override void Update()
    {
        base.Update();
        if(m_currentHealth <= 0)
        {
            m_animator.SetTrigger("Deaded");
            GameManager.Instance.m_loseScreen.SetActive(true);
        }
    }

    public void StartPlayerTurn()
    {
        GameManager.Instance.m_gameScreen.SetActive(true);
        if(m_currentEnergy < m_energyLevel)
        {
            m_currentEnergy = m_energyLevel;
        }
        m_drawPileManager.DrawCardsForTurn();
        GameManager.Instance.m_uIManager.UpdateText();
    }

    public void ResetPlayerBuffs()
    {
        m_utility = 0;
        m_strength = 0;
    }

    public void ResetPlayerValues()
    {
        m_maxHealth = m_startingHealth;
        m_currentHealth = m_startingHealth;
        m_money = m_startingMoney;
    }
}
