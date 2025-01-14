using UnityEngine;

public class Character : MonoBehaviour
{
    protected int m_strength;
    protected int m_utility;


    protected int m_health;
    protected int m_maxHealth;



    #region repeatable functions
    public void IncreaseMaxHealth()
    {
        m_maxHealth += 10;
        m_health += 10;
    }

    public int RestoreHealth (int healthGain)
    {
        return healthGain;
    }
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
