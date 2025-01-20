using TMPro;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private TMP_Text m_moneyText;
    [SerializeField]
    private TMP_Text m_healthText;
    [SerializeField]
    private TMP_Text m_armorText;
    [SerializeField]
    private GameObject m_armorUI;


    private int m_startingMoney = 50;
    public int m_money;

    public int m_currentEnergy;
    public int m_energyLevel = 3;

    public int m_maxHealth = 50;
    public int m_startingHealth = 50;

    private DrawPileManager m_drawPileManager;
    private void Start()
    {
        m_money = m_startingMoney;
        m_currentHealth = m_maxHealth;
        m_drawPileManager = FindFirstObjectByType<DrawPileManager>();
        UpdateText();
    }

    #region Armor + Health
    public void IncreaseArmor(int armorIncrease)
    {
        m_armor += armorIncrease;
        UpdateText();
    }

    public void ResetArmor()
    {
        m_armor = 0;
    }

    public void IncreaseMaxHealth()
    {
        m_maxHealth += 10;
        m_currentHealth += 10;
        UpdateText();
    }
    #endregion

    void Update()
    {
        if(m_currentHealth <= 0)
        {
            GameManager.Instance.m_loseScreen.SetActive(true);
        }

        if(m_armor > 0)
        {
            m_armorUI.SetActive(true);
        }
        else
        {
            m_armorUI.SetActive(false);
        }
    }

    public void StartPlayerTurn()
    {
        if(m_currentEnergy < m_energyLevel)
        {
            m_currentEnergy = m_energyLevel;
        }
        m_drawPileManager.DrawCardsForTurn();
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

    //Seperate functions?
    private void UpdateText()
    {
        m_healthText.text = m_currentHealth.ToString() + " / " + m_maxHealth.ToString();
        m_moneyText.text = m_money.ToString();
        m_armorText.text = m_armor.ToString();
    }
}
