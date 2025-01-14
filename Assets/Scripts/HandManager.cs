using UnityEngine;
using System.Collections.Generic;
using ScriptableCard;

public class HandManager : MonoBehaviour
{
    //Assign card prefab in inspector.
    public GameObject m_cardPrefab;
    //Root of the hand position.
    public Transform m_handTransform;
    //How far the hand will be spread out.
    public float m_fanSpread = 7.5f;
    //Horizontal space between the cards in units.
    public float m_horizontralCardSpacing = -100f;
    //Vertical space between the cards in units.
    public float m_verticalCardSpacing = 100f;

    public int m_maxHandSize;

    //Hold a list of card objects in hand.
    public List<GameObject> m_cardsInHand = new List<GameObject>();
    

    public void AddCardToHand(Card cardData)
    {
        if (m_cardsInHand.Count < m_maxHandSize)
        {
            //Instantiate the card.
            GameObject newCard = Instantiate(m_cardPrefab, m_handTransform.position, Quaternion.identity, m_handTransform);
            //Adds the new instantiated card to hand.
            m_cardsInHand.Add(newCard);

            //Set the CardData of the instantiated card
            newCard.GetComponent<CardDisplay>().m_cardData = cardData;

            //Updates hand visuals.
            UpdateHandVisuals();
        }
    }

    private void Update()
    {
        /*UpdateHandVisuals();*/
    }

    public void BattleSetup(int setMaxHandSize)
    {
        m_maxHandSize = setMaxHandSize;
    }

    private void UpdateHandVisuals() 
    { 
        int cardCount = m_cardsInHand.Count;

        //Got an error while trying to divide by 0
        if(cardCount == 1)
        {
            m_cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            m_cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        //To give the cards a proper position and rotation for a good looking hand.
        for(int i = 0; i < cardCount; i++)
        {
            // Calculating the rotation of every card in hand for proper visuals.
            float rotationAngle = (m_fanSpread * (i - (cardCount - 1) / 2f));
            m_cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (m_horizontralCardSpacing * (i - (cardCount - 1) / 2f));
            //Nomalizes cardpositions between -1 and 1.
            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset =   m_verticalCardSpacing * (1 - normalizedPosition * normalizedPosition);

            //Set card positions
            m_cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
