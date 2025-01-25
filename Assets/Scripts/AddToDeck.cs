using ScriptableCard;
using UnityEngine;

public class AddToDeck : MonoBehaviour, IPickUpAble
{
    [SerializeField]
    private GameObject m_CardPrefab;

    private CardDisplay m_cardDisplay;
    private DrawPileManager m_pileManager;
    private Player m_player;
    private Card m_card;

    private void Awake()
    {
        m_pileManager = FindFirstObjectByType<DrawPileManager>();
    }

    public void CheckMoney()
    {
        m_player = FindFirstObjectByType<Player>();
        m_cardDisplay = GetComponentInChildren<CardDisplay>();

        if(m_player.m_money >= m_cardDisplay.m_cardData.m_shopValue)
        {
            m_player.m_money -= m_cardDisplay.m_cardData.m_shopValue;
            PickUp();
            GameManager.Instance.m_uIManager.UpdateText();
        }
    }

    public void PickUp()
    {
        m_card = GetComponentInChildren<CardDisplay>().m_cardData;
        m_pileManager.m_currentDeck.Add(m_card);
    }
}
