using UnityEngine;


public enum CardType
{
    Damage,
    Utility,
    Debuff
}
namespace ScriptableCard{

    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string m_cardName;
        public string m_cardDescription;

        public CardType m_cardType;

        public int m_cardCost;
        public int m_cardDamage;
        public int m_cardHeal;


        public Sprite m_cardSprite;
    }
}

