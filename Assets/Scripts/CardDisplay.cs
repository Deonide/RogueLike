using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableCard;



public class CardDisplay : MonoBehaviour
{
    [Header("Images")]
    [SerializeField]
    private Image m_cardBackground;
    [SerializeField]
    private Image m_cardImage;
    [SerializeField]
    private Image[] m_backgrounds;

    [Header("TMPRO")]
    [SerializeField]
    public TMP_Text m_cost;
    [SerializeField]
    private TMP_Text m_name;
    [SerializeField]
    private TMP_Text m_type;
    [SerializeField]
    private TMP_Text m_description;


/*    [SerializeField]
    public TMP_Text m_damage;
    [SerializeField]
    public TMP_Text m_armor;*/


    [Header("Scriptable Object")]
    public Card m_cardData;

    void Start()
    {
        //Updates the card values of the card
        UpdateCardDisplay(); 
    }

    public void UpdateCardDisplay()
    {
        //Card name
        m_name.text = m_cardData.m_cardName;
        //Card cost
        m_cost.text = m_cardData.m_cardCost.ToString();
        //Card type
        m_type.text = m_cardData.m_cardType.ToString();
        //Card description
/*        m_description.text = m_cardData.m_cardDescription.ToString();*/
/*        //Card damage
        m_damage.text = m_cardData.m_cardDamage.ToString();
        //Card healing
        m_heal.text = m_cardData.m_cardHeal.ToString();*/
        //Card image
        m_cardImage.sprite = m_cardData.m_cardSprite;



        if(m_cardData.m_cardType == CardType.Damage)
        {
            m_cardBackground = m_backgrounds[0];
        }
    }
}
