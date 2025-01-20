using UnityEngine;
using System.Collections.Generic;
using ScriptableCard;
using TMPro;

public class DiscardManager : MonoBehaviour
{
    public List<Card> m_discardedCards = new List<Card>();

    public TextMeshProUGUI m_discardCount;

    public int m_discardCardsCount;

    private void Awake()
    {
        UpdateDiscardCount();
    }

    public void UpdateDiscardCount()
    {
        m_discardCount.text = m_discardedCards.Count.ToString();
        m_discardCardsCount = m_discardedCards.Count;
    }

    public void AddToDiscard(Card card)
    {
        if(card != null)
        {
            m_discardedCards.Add(card);
            UpdateDiscardCount();
        }
    }

    public Card PullFromDiscard()
    {
        if(m_discardCardsCount > 0)
        {
            Card cardToReturn = m_discardedCards[m_discardCardsCount - 1];
            UpdateDiscardCount();
            return cardToReturn;
        }
        else
        {
            return null;
        }
    }

    public bool PullSelectedCardFromDiscard(Card card)
    {
        if(m_discardCardsCount > 0 && m_discardedCards.Contains(card))
        {
            m_discardedCards.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Card> PullAllFromDiscard()
    {
        if(m_discardCardsCount > 0)
        {
            Debug.Log("add to deck");
            List<Card> cardsToReturn = new List<Card>(m_discardedCards);
            m_discardedCards.Clear();
            UpdateDiscardCount();
            return cardsToReturn;
        }

        else
        {
            return new List<Card>();
        }
    }

}
