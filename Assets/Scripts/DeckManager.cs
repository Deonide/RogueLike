using System.Collections.Generic;
using UnityEngine;
using ScriptableCard;
using UnityEngine.XR;

public class DeckManager : MonoBehaviour
{
    public List<Card> m_allCards = new List<Card>();
    public int m_startingHandSize = 3;
    public int m_maxHandSize = 10;
    public int m_currentHandSize;

    private HandManager m_handManager;
    private DrawPileManager m_drawPileManager;
    private bool m_startBattleRun = true;

    private void Start()
    {
        //Load all the card assets from the resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add all loaded cards to the allCards List.
        m_allCards.AddRange(cards);
    }

    private void Awake()
    {
        if(m_drawPileManager == null)
        {
            m_drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }

        if (m_handManager == null)
        {
            m_handManager = FindFirstObjectByType<HandManager>();
        }
    }

    private void Update()
    {
        if (m_startBattleRun)
        {
            BattleSetUp();
        }
    }

    public void BattleSetUp()
    {
        m_handManager.BattleSetup(m_maxHandSize);
        /*        m_drawPileManager.MakeDrawPile(m_allCards);*/
        m_drawPileManager.BattleSetup(m_startingHandSize, m_maxHandSize);
        m_startBattleRun = false;  
    }
}
