using NUnit.Framework;
using System.Collections.Generic;
using ScriptableCard;
using UnityEngine;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{
    [Header("Card Prefab + Spawn")]
    [SerializeField]
    private GameObject m_cardPrefab;
    [SerializeField]
    private GameObject[] m_spawnPos;
    [SerializeField]
    private List<Card> m_cardsInShop;

    [Header ("Common Cards")]
    [SerializeField]
    private List<Card> m_commonCards;

    [Header("Rare Cards")]
    [SerializeField]
    private List<Card> m_rareCards;

    [Header("Epic Cards")]
    [SerializeField]
    private List<Card> m_epicCards;

    [Header("Legendary Cards")]
    [SerializeField]
    private List<Card> m_legendaryCards;

    [Header("Buy Button")]
    [SerializeField]
    private TMP_Text[] m_buyButton;

    private GameObject m_spawnedCard;

    private int m_rarityDrop;
    private int m_cardsToDrop = 5;

    public int m_timesHealthPurchased;
    public int m_timesEnergyPurchased;
    public int m_upgradeCost = 250;

    private DrawPileManager m_DrawPileManager;
    private Player m_player;
    private AddToDeck m_toDeck;
    private Card m_card;

    private void Awake()
    {
        m_DrawPileManager = FindFirstObjectByType<DrawPileManager>();
        m_player = FindFirstObjectByType<Player>();
    }


    public void RarityCheck()
    {
        for (int i = 0; i < m_cardsToDrop; i++)
        {
            m_rarityDrop = Random.Range(0, 201);
            m_spawnedCard = Instantiate(m_cardPrefab, m_spawnPos[i].transform.position, Quaternion.identity, m_spawnPos[i].transform);
            if (m_rarityDrop <= 100)
            {
                int cardToDrop = Random.Range(0, m_commonCards.Count);
                UpdateCardData(m_spawnedCard, m_commonCards.ElementAt(cardToDrop));
            }
            else if (m_rarityDrop > 100 && m_rarityDrop <= 190)
            {
                int cardToDrop = Random.Range(0, m_rareCards.Count);
                UpdateCardData(m_spawnedCard, m_rareCards.ElementAt(cardToDrop));
            }
            else if (m_rarityDrop > 190 && m_rarityDrop <= 199)
            {
                int cardToDrop = Random.Range(0, m_epicCards.Count);
                UpdateCardData(m_spawnedCard, m_epicCards.ElementAt(cardToDrop));
            }
            else
            {
                int cardToDrop = Random.Range(0, m_legendaryCards.Count);
                UpdateCardData(m_spawnedCard, m_legendaryCards.ElementAt(cardToDrop));
            }
            m_buyButton[i].text = m_spawnedCard.GetComponent<CardDisplay>().m_cardData.m_shopValue.ToString();
            
        }
    }

    private void UpdateCardData(GameObject game, Card cardData)
    {
        //Set the CardData of the instantiated card
        game.GetComponent<CardDisplay>().m_cardData = cardData;
        game.GetComponent<CardDisplay>().UpdateCardDisplay();
        m_cardsInShop.Add(cardData);
    }

    public void PickUp()
    {
        m_DrawPileManager = FindFirstObjectByType<DrawPileManager>();
        m_DrawPileManager.m_currentDeck.Add(m_card);
    }

    public void HealthUpgrade()
    {
        if(m_timesHealthPurchased < 5 && m_player.m_money >= m_upgradeCost)
        {
            m_player.m_maxHealth *= 2;
            m_player.m_money -= m_upgradeCost;
            m_player.m_currentHealth += m_player.m_maxHealth / 2;
            m_upgradeCost *= 2;
            UpdateCostText();
            GameManager.Instance.m_uIManager.UpdateText();
            m_timesHealthPurchased += 1;
        }
    }

    public void EnergyUpgrade()
    {
        if (m_timesHealthPurchased < 5 && m_player.m_money >= m_upgradeCost)
        {
            m_player.m_energyLevel += 1;
            m_player.m_money -= m_upgradeCost;
            m_upgradeCost *= 2;
            UpdateCostText();
            GameManager.Instance.m_uIManager.UpdateText();
            m_timesEnergyPurchased += 1;
        }
    }

    private void UpdateCostText()
    {
        GameManager.Instance.m_uIManager.m_healthUpgradeCost.text = m_upgradeCost.ToString();
        GameManager.Instance.m_uIManager.m_energyUpgradeCost.text = m_upgradeCost.ToString();
    }
}
