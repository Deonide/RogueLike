using System.Linq;
using UnityEngine;


public enum CardType
{
    Damage,
    Utility,
    Debuff
}

public enum SecondaryType
{
    None,
    Armor,
    Buff,
    Poison,
    Debuff
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

namespace ScriptableCard{

    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string m_cardName;
        public string m_cardDescription;

        public CardType m_cardType;
        public SecondaryType m_cardSecondaryType;
        public Rarity m_cardRarity;


        public int m_cardCost;
        public int m_cardDamage;
        public int m_cardArmor;
        public int m_poisonValue;
        public int m_weak;
        public int m_vulnerable;

        public int m_strengthBuff;
        public int m_utilityBuff;

        public int m_shopValue;

        public Sprite m_cardSprite;

        public string GetDescription()
        {
            string[] _words = m_cardDescription.Split(' ');
            for(int i = 0; i < _words.Length; i++)
            {
                switch(_words[i])
                {
                    case "$damage":
                        _words[i] = m_cardDamage.ToString();
                        break;
                    case "$armor":
                        _words[i] = m_cardArmor.ToString();
                        break;
                    case "$poison":
                        _words[i] = m_poisonValue.ToString();
                        break;
                }
            }
            return string.Join(' ', _words);
        }
    }
}

