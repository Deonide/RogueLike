using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Text m_moneyText;
    [SerializeField]
    private TMP_Text m_armorText;
    [SerializeField]
    private TMP_Text m_energyText;
    [SerializeField]
    private GameObject m_armorUI;
    [SerializeField]
    private TMP_Text m_healthText;
    
    public TMP_Text m_energyUpgradeCost, m_healthUpgradeCost;

    private Player m_player;
    void Start()
    {
        m_player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        if (m_player.m_armor > 0)
        {
            m_armorUI.SetActive(true);
        }
        else
        {
            m_armorUI.SetActive(false);
        }
    }

    public void UpdateText()
    {
        m_healthText.text = m_player.m_currentHealth.ToString() + " / " + m_player.m_maxHealth.ToString();
        m_energyText.text = m_player.m_currentEnergy.ToString() + " / " + m_player.m_energyLevel.ToString();
        m_moneyText.text = m_player.m_money.ToString();
        m_armorText.text = m_player.m_armor.ToString();
    }
}
