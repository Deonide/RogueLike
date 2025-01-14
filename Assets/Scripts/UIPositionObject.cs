using UnityEngine;

public class UIObjectPositioner : MonoBehaviour
{
    public RectTransform objectToPosition;

    [Header("Width")]
    public int m_widthDivider = 2;
    public float m_widthMultiplier = 1f;
    [Header("Height")]
    public int m_heightDivider = 2;
    public float m_heightMultiplier = 1f;
    [Header("Editor")]
    public bool m_updatePosition = false;

    void Start()
    {
        SetUIObjectPosition();
    }

    void Update()
    {
        if (m_updatePosition)
        {
            SetUIObjectPosition();
        }
    }

    public void SetUIObjectPosition()
    {
        if (objectToPosition != null && m_widthDivider != 0 && m_heightDivider != 0)
        {
            // Calculate the anchor position
            float anchorX = m_widthMultiplier / m_widthDivider;
            float anchorY = m_heightMultiplier / m_heightDivider;

            // Set the anchor and pivot
            objectToPosition.anchorMin = new Vector2(anchorX, anchorY);
            objectToPosition.anchorMax = new Vector2(anchorX, anchorY);
            objectToPosition.pivot = new Vector2(0.5f, 0.5f);

            // Set the local position to zero to align with the anchor point
            objectToPosition.anchoredPosition = Vector2.zero;
        }
    }
}
