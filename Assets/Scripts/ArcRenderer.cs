using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

// Tutorial - Deckbuilding Card Game Ep. 6 - Arc Renderer.
// https://www.youtube.com/watch?v=1pY8WENWpg0&list=PLK8cTgLAUsQHkfCF73d43jhn185fu5h5c&index=8
public class ArcRenderer : MonoBehaviour
{
    [SerializeField]
    private float m_spacingScale;

    public GameObject m_arrowPrefab;
    public GameObject m_dotPrefab;
    public int m_dotPoolSize = 50;

    private List<GameObject> m_dotPool = new List<GameObject>();
    private GameObject m_arrowInstance;

    public float m_spacing = 50;
    public float m_arrowAngleAdjustment = 0;
    public int m_dotsToSkip = 1;
    private Vector3 m_arrowDirection;

    public float m_baseScreenWidth = 1920;

    private void Start()
    {
        m_arrowInstance = Instantiate(m_arrowPrefab, transform);
        m_arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(m_dotPoolSize);

        m_spacingScale = Screen.width / m_baseScreenWidth;
    }

    private void OnEnable()
    {
        m_spacingScale = Screen.width / m_baseScreenWidth;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 0;

        Vector3 startPos = transform.position;
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

        UpdateArc(startPos, midPoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    private void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / (m_spacing * m_spacingScale));

        for (int i = 0; i < numDots && i < m_dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f);

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);

            if(i != numDots - m_dotsToSkip)
            {
                m_dotPool[i].transform.position = position;
                m_dotPool[i].SetActive(true);
            }

            if(i == numDots - (m_dotsToSkip + 1) && i - m_dotsToSkip + 1 >= 0)
            {
                m_arrowDirection = m_dotPool[i].transform.position;
            }
        }

        //Deactivate Unused dots
        for(int i = numDots - m_dotsToSkip; i < m_dotPool.Count; i++) 
        { 
            if(i > 0)
            {
                m_dotPool[i].SetActive(false);
            }
        }
    }

    private void PositionAndRotateArrow(Vector3 position)
    {
        m_arrowInstance.transform.position = position;
        Vector3 direction = m_arrowDirection - position;
        //Mathf.Atan2 converts an x and y position into radiants? (Returns a value of negative 3,14... and 3.14... (3.14... = pie).
        //Then when multiplied (with Mathf.Rad2Deg) converts from radiants to degrees.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Doing some adjustments to the rotation of the arrow instance.
        angle += m_arrowAngleAdjustment;
        //Quaternion.AngleAxis takes 2 inputs first is the angle and the other is the axis that we want to change(any axis that has a one is the axis we will change).
        m_arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 midpoint = (start + end) / 2;
        float arcHeight = Vector3.Distance(start, end) / 3f;
        midpoint.y += arcHeight;
        return midpoint;
    }

    private Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;
    }

    private void InitializeDotPool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject dot = Instantiate(m_dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            m_dotPool.Add(dot);
        }
    }
}
