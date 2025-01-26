using NUnit.Framework;
using System.Collections.Generic;
using ScriptableCard;
using UnityEngine;
using System.Linq;
using TMPro;

public class LootManager : MonoBehaviour
{
    [Header("Card Prefab + Spawn")]
    [SerializeField]
    private GameObject m_cardPrefab;
    [SerializeField]
    private GameObject[] m_spawnPos;

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

    [Header("Buttons")]
    [SerializeField]
    private GameObject[] m_button;

    private GameObject m_spawnedCard;

    private int m_rarityDrop;
    private int m_cardsToDrop = 3;
    private int m_GoldToDrop;

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
        }

        foreach(GameObject button in m_button)
        {
            button.SetActive(true);
        }
    }

    public void MoneyDrop()
    {
        m_GoldToDrop = Mathf.RoundToInt(15 * GameManager.Instance.m_waveManager.m_amountOfEnemies);
        GameManager.Instance.m_player.m_money += m_GoldToDrop;
        GameManager.Instance.m_uIManager.UpdateText();
    }

    private void UpdateCardData(GameObject game, Card cardData)
    {
        //Set the CardData of the instantiated card
        game.GetComponent<CardDisplay>().m_cardData = cardData;
        game.GetComponent<CardDisplay>().UpdateCardDisplay();
    }
}
