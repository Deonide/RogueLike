using UnityEngine;

public class Player : Character
{
    public int m_currentEnergy;
    private int m_energyLevel = 3;


    public int m_startingMoney = 50;
    public int m_money;

    void Start()
    {
        m_money = m_startingMoney;
    }

    void Update()
    {
        
    }

    public void StartPlayerTurn()
    {
        if(m_currentEnergy < m_energyLevel)
        {
            m_currentEnergy = m_energyLevel;
        }
    }
}
