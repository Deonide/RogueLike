using UnityEngine;
using ScriptableCard;
using System.Collections.Generic;
using TMPro;
using Shuffle;


public class DrawPileManager : MonoBehaviour
{
    public List<Card> m_currentDeck = new List<Card>();
    public List<Card> m_starterDeck = new List<Card>();

    public int m_currentIndex = 0;
    public int m_startingHandSize = 5;
    public int m_maxHandSize = 10;
    public int m_currentHandSize;

    private HandManager m_handManager;
    private DiscardManager m_discardManager;
    public TextMeshProUGUI m_drawPileCounter;

    private void Start()
    {
        m_handManager = FindFirstObjectByType<HandManager>();
        m_discardManager = FindFirstObjectByType<DiscardManager>();
        MakeDrawPile(m_starterDeck);
    }

    public void DrawCardsForTurn()
    {
        for (int i = 0; i < m_startingHandSize; i++)
        {
            DrawCard(m_handManager);
        }
    }

    private void Update()
    {
        if (m_handManager != null)
        {
            m_currentHandSize = m_handManager.m_cardsInHand.Count;
        }
    }

    public void ResetDeck()
    {
        m_currentDeck.Clear();
        m_discardManager.m_discardedCards.Clear();
        m_discardManager.UpdateDiscardCount();
        m_handManager.m_cardsInHand.Clear();
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        m_currentDeck.AddRange(cardsToAdd);
        Utility.Shuffle(m_currentDeck);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int NumberOfCardToDraw, int setMaxHandSize)
    {
        m_maxHandSize = setMaxHandSize;
        DrawCardsForTurn();
    }

    public void DrawCard(HandManager handManager)
    {
        if (m_currentDeck.Count == 0)
        {
            RefillDeckFromDiscard();
        }

        if (m_currentHandSize < m_maxHandSize)
        {
            Card nextCard = m_currentDeck[m_currentIndex];
            handManager.AddCardToHand(nextCard);
            m_currentDeck.RemoveAt(m_currentIndex);
            UpdateDrawPileCount();
            if(m_currentDeck.Count > 0)
            {
                m_currentIndex = (m_currentIndex + 1) % m_currentDeck.Count;
            }
        }
    }

    public void RefillDeckFromDiscard()
    {
        if(m_discardManager == null)
        {
            m_discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if(m_discardManager != null && m_discardManager.m_discardCardsCount > 0)
        {
            m_currentDeck = m_discardManager.PullAllFromDiscard();
            Utility.Shuffle(m_currentDeck);
            m_currentIndex = 0;
        }
    }

    public void RestartDeckAtNewWave()
    {
        if (m_discardManager == null)
        {
            m_discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if (m_discardManager != null && m_discardManager.m_discardCardsCount > 0)
        {
            m_currentDeck.AddRange(m_discardManager.PullAllFromDiscard());
            Utility.Shuffle(m_currentDeck);
            m_currentIndex = 0;
        }
    }

    private void UpdateDrawPileCount()
    {
        m_drawPileCounter.text = m_currentDeck.Count.ToString();
    }
}
