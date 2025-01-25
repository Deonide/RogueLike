using ScriptableCard;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header ("Effects")]
    //For chaging the size of the card.
    [SerializeField] 
    private float m_selectScale = 1.1f;
    [SerializeField] 
    private Vector2 m_cardPlay;
    //Jumps to position when trying to play a card.
    [SerializeField] 
    private Vector3 m_playPosition;
    [SerializeField] 
    //Highlight card effect.
    private GameObject m_glowEffect;
    [SerializeField] 
    private GameObject m_playArrow;
    [SerializeField] 
    private float m_lerpFactor = 0.1f;

    [Header("Play Position")]
    [SerializeField] 
    private int m_cardPlayDivider = 4;
    [SerializeField] 
    private float m_cardPlayMultiplier = 1f;
    [SerializeField] 
    private int m_playPositionYDivider = 2;
    [SerializeField] 
    private float m_playPositionYMultiplier = 1f;
    [SerializeField] 
    private int m_playPositionXDivider = 4;
    [SerializeField] 
    private float m_playPositionXMultiplier = 1f;

    [Header("Editor")]
    [SerializeField] 
    private bool m_needUpdatePlayPosition = false;
    [SerializeField]
    private bool m_needUpdateCardPlayPosition = false;

    [SerializeField]
    private LayerMask m_enemyLayerMask;

    //RectTransform of card prefab
    private RectTransform m_rectTransform;
    //Parent Canvas
    private Canvas m_canvas;
    private RectTransform m_canvasRectTranform;
    private Vector3 m_originalScale;
    private int m_currentState = 0;

    //Original position and rotation of the card prefab.
    private Quaternion m_originalRotation;
    private Vector3 m_originalPosition;

    public Card m_cardData;
    private CardDisplay m_cardDisplay;
    private HandManager m_handManager;
    private DiscardManager m_discardManager;
    private Player m_player;

    private void Awake()
    {
        m_handManager = FindFirstObjectByType<HandManager>();
        m_discardManager = FindFirstObjectByType<DiscardManager>();
        m_player = FindFirstObjectByType<Player>();
        m_cardDisplay = GetComponent<CardDisplay>();
        m_rectTransform = GetComponent<RectTransform>();
        
        m_canvas = GetComponentInParent<Canvas>();
        if (m_canvas != null)
        {
            m_canvasRectTranform = m_canvas.GetComponent<RectTransform>();
        }

        m_originalScale = m_rectTransform.localScale;
        m_originalPosition = m_rectTransform.localPosition;
        m_originalRotation = m_rectTransform.localRotation;

        UpdateCardPlayPostion();
        UpdatePlayPostion();
    }

    private void Update()
    {
        if (m_needUpdateCardPlayPosition)
        {
            UpdateCardPlayPostion();
        }

        if (m_needUpdatePlayPosition)
        {
            UpdatePlayPostion();
        }

        switch (m_currentState)
        {
            case 1:
                HandleHoverState();
                break;

            case 2:
                HandleDragState();
                //Check if mouse button is released
                if (!Input.GetMouseButton(0))
                {
                    TransitionToStateZero();
                }
                break;

            case 3:
                HandlePlayState();
                //Check if mouse button is released
                if (!Input.GetMouseButton(0))
                {
                    TransitionToStateZero();
                }
                break;
        }
    }


    //Sets everything back to zero.
    private void TransitionToStateZero()
    {
        m_currentState = 0;
        //Reset Scale.
        m_rectTransform.localScale = m_originalScale;
        //Reset Rotation.
        m_rectTransform.localRotation = m_originalRotation;
        //Reset Position.
        m_rectTransform.localPosition = m_originalPosition;
        //Disable glow effect.
        m_glowEffect.SetActive(false);
        //Disable playArrow.
        m_playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_currentState == 0)
        {
            //Sets original local values;
            m_originalPosition = m_rectTransform.localPosition;
            m_originalRotation = m_rectTransform.localRotation;
            m_originalScale = m_rectTransform.localScale;

            m_currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(m_currentState == 1) 
        {
            TransitionToStateZero();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(m_currentState == 1)
        {
            m_currentState = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_currentState == 2)
        {      
            if (m_rectTransform.localPosition.y > m_cardPlay.y)
            {
                m_currentState = 3;
                m_playArrow.SetActive(true);
                m_rectTransform.localPosition = Vector3.Lerp(m_rectTransform.position, m_playPosition, m_lerpFactor);
            }
        }
    }

    private void HandleHoverState()
    {
        m_glowEffect.SetActive(true);
        m_rectTransform.localScale = m_originalScale * m_selectScale; 
    }

    private void HandleDragState()
    {
        //Set the card's rotation to zero
        m_rectTransform.localRotation = Quaternion.identity;
        m_rectTransform.position = Vector3.Lerp(m_rectTransform.position, Input.mousePosition, m_lerpFactor);
    }
    
    private void HandlePlayState()
    {
        m_rectTransform.localPosition = m_playPosition;
        m_rectTransform.localRotation = Quaternion.identity;

        if (!Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(m_player.m_currentEnergy >= m_cardDisplay.m_cardData.m_cardCost)
            {
                if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                {
                    EnemyBase enemyScript = hit.collider.GetComponent<EnemyBase>();
                    if(m_cardDisplay.m_cardData.m_cardType == CardType.Damage)
                    {
                        #region Damage
                        if (m_player.m_isWeak == true && enemyScript.m_isVulnerable == true)
                        {
                            //Deals damage equal to the cards damage.
                            enemyScript.Damage(m_cardDisplay.m_cardData.m_cardDamage + m_player.m_strength);
                        }
                        else if(enemyScript.m_isVulnerable == true)
                        {
                            //If the enemy is vulnerable it takes 25% more damage.
                            int damage = Mathf.RoundToInt((m_cardDisplay.m_cardData.m_cardDamage + m_player.m_strength) * 1.25f);
                            enemyScript.Damage(damage);
                        }
                        else if(m_player.m_isWeak == true )
                        {
                            //If the player is weak the player deals 25% less damage.
                            int damage = Mathf.RoundToInt((m_cardDisplay.m_cardData.m_cardDamage + m_player.m_strength) * 0.75f);
                            Debug.Log(damage);
                            enemyScript.Damage(damage);
                        }
                        else
                        {
                            enemyScript.Damage(m_cardDisplay.m_cardData.m_cardDamage + m_player.m_strength);
                        }
                        #endregion
                        #region Secondary Effect
                        if (m_cardDisplay.m_cardData.m_cardSecondaryType == SecondaryType.Poison)
                        {
                            //Increases poisonValue of the enemy
                            enemyScript.m_poisonValue += m_cardDisplay.m_cardData.m_poisonValue;
                        }
                        else if(m_cardDisplay.m_cardData.m_cardSecondaryType == SecondaryType.Armor)
                        {
                            m_player.IncreaseArmor(m_cardDisplay.m_cardData.m_cardArmor + m_player.m_utility);
                        }
                        else if(m_cardDisplay.m_cardData.m_cardSecondaryType == SecondaryType.Debuff)
                        {
                            enemyScript.m_weakValue += m_cardDisplay.m_cardData.m_weak;
                            enemyScript.m_vulnerableValue += m_cardDisplay.m_cardData.m_vulnerable;
                        }
                        #endregion
                        if (enemyScript.m_spikeValue > 0)
                        {
                            m_player.Damage(enemyScript.m_spikeValue);
                            enemyScript.ArmorCheck(enemyScript.m_spikeValue);
                        }

                        CardPlayed();
                    }

                    else if(m_cardDisplay.m_cardData.m_cardType == CardType.Debuff)
                    {
                        enemyScript.m_poisonValue += m_cardData.m_poisonValue;
                        enemyScript.m_weakValue += m_cardData.m_weak;
                        enemyScript.m_vulnerableValue += m_cardData.m_vulnerable;
                        CardPlayed();
                    }
                }

                else if (hit.collider != null && hit.collider.CompareTag("Player") && m_cardDisplay.m_cardData.m_cardType == CardType.Utility)
                {
                    m_player.IncreaseArmor(m_cardDisplay.m_cardData.m_cardArmor + m_player.m_utility);
                    m_player.m_strength += m_cardDisplay.m_cardData.m_strengthBuff;
                    CardPlayed();
                }
            }
        }

        if(Input.mousePosition.y < m_cardPlay.y)
        {
            m_currentState = 2;
            m_playArrow.SetActive(false);
        }
    }

    private void CardPlayed()
    {
        m_handManager = FindFirstObjectByType<HandManager>();

        m_player.m_currentEnergy -= m_cardDisplay.m_cardData.m_cardCost;
        GameManager.Instance.m_uIManager.UpdateText();
        m_handManager.m_cardsInHand.Remove(gameObject);
        m_handManager.UpdateHandVisuals();
        DiscardManager discardManager = FindFirstObjectByType<DiscardManager>();
        discardManager.AddToDiscard(GetComponent<CardDisplay>().m_cardData);
        Destroy(gameObject);
    }

    private void UpdateCardPlayPostion()
    {
        if (m_cardPlayDivider != 0 && m_canvasRectTranform != null)
        {
            float segment = m_cardPlayMultiplier / m_cardPlayDivider;

            m_cardPlay.y = m_canvasRectTranform.rect.height * segment;
        }
    }

    private void UpdatePlayPostion()
    {
        if (m_canvasRectTranform != null && m_playPositionYDivider != 0 && m_playPositionXDivider != 0)
        {
            float segmentX = m_playPositionXMultiplier / m_playPositionXDivider;
            float segmentY = m_playPositionYMultiplier / m_playPositionYDivider;

            m_playPosition.x = m_canvasRectTranform.rect.width * segmentX;
            m_playPosition.y = m_canvasRectTranform.rect.height * segmentY;
        }
    }
}
