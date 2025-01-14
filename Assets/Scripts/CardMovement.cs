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


    private void Awake()
    {
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

        if(Input.mousePosition.y < m_cardPlay.y)
        {
            m_currentState = 2;
            m_playArrow.SetActive(false);
            DiscardManager discardManager = FindFirstObjectByType<DiscardManager>();
            discardManager.AddToDiscard(GetComponent<CardDisplay>().m_cardData);
            Destroy(gameObject);
        }
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
