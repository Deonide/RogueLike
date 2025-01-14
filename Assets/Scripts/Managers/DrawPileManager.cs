using UnityEngine;
using ScriptableCard;
using System.Collections.Generic;
using TMPro;
using Shuffle;


public class DrawPileManager : MonoBehaviour
{
    public List<Card> m_drawPile = new List<Card>();

    public int m_currentIndex = 0;
    public int m_startingHandSize = 3;
    public int m_maxHandSize;
    public int m_currentHandSize;


    private HandManager m_handManager;
    private DiscardManager m_discardManager;
    public TextMeshProUGUI m_drawPileCounter;


    private void Start()
    {
        m_handManager = FindFirstObjectByType<HandManager>();
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

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        m_drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(m_drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int NumberOfCardToDraw, int setMaxHandSize)
    {
        m_maxHandSize = setMaxHandSize;
        DrawCardsForTurn();
    }

    public void DrawCard(HandManager handManager)
    {
        if (m_drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }

        if (m_currentHandSize < m_maxHandSize)
        {
            Card nextCard = m_drawPile[m_currentIndex];
            handManager.AddCardToHand(nextCard);
            m_drawPile.RemoveAt(m_currentIndex);
            UpdateDrawPileCount();
            if(m_drawPile.Count > 0)
            {
                m_currentIndex = (m_currentIndex + 1) % m_drawPile.Count;
            }
        }
    }

    private void RefillDeckFromDiscard()
    {
        if(m_discardManager == null)
        {
            m_discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if(m_discardManager != null && m_discardManager.m_discardCardsCount > 0)
        {
            m_drawPile = m_discardManager.PullAllFromDiscard();
            Utility.Shuffle(m_drawPile);
            m_currentIndex = 0;
        }
    }

    private void UpdateDrawPileCount()
    {
        m_drawPileCounter.text = m_drawPile.Count.ToString();

    }

}
