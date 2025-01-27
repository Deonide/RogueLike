using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using ScriptableCard;
using Unity.VisualScripting;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_enemyPrefabs;

    [SerializeField]
    private GameObject m_shopButton, m_cardDropButton;

    [SerializeField]
    private TextMeshProUGUI m_waveCounter;

    public List<GameObject> m_spawnedEnemies = new List<GameObject>();
    public List<GameObject> m_spawnLocation = new List<GameObject>();

    private int m_spawnedEnemiesLoop;
    public float m_amountOfEnemies;

    private HandManager m_handManager;

    private bool m_buttonSpawned;

    private void Start()
    {
        m_handManager = FindFirstObjectByType<HandManager>();
        m_shopButton.SetActive(false);
        Spawner();
    }

    private void Update()
    {
        if(m_spawnedEnemies.Count == 0)
        {
            m_handManager.DiscardHand();
            GameManager.Instance.m_winScreen.SetActive(true);
            GameManager.Instance.m_gameScreen.SetActive(false);
            if (!m_buttonSpawned)
            {
                Debug.Log(GameManager.Instance.m_currentWave % 1 == 0);
                m_cardDropButton.SetActive(true);
                if(GameManager.Instance.m_currentWave % 1 == 0)
                {
                    m_shopButton.SetActive(true);
                }
                m_buttonSpawned = true;
            }
        }
    }

    public void Spawner()
    {
        m_amountOfEnemies = 1 + Mathf.Round(GameManager.Instance.m_currentWave / 5);
        if (m_amountOfEnemies > 4)
        {
            m_amountOfEnemies = 4;
        }

        GameManager.Instance.m_currentWave++;
        WaveCounter();
        for (m_spawnedEnemiesLoop = 0; m_spawnedEnemiesLoop < m_amountOfEnemies; m_spawnedEnemiesLoop++)
        {
            int randomSpawn = Random.Range(0, m_enemyPrefabs.Length);
            GameObject enemyToSpawn = m_enemyPrefabs[randomSpawn];
            GameObject enemySpawned = Instantiate(enemyToSpawn, m_spawnLocation[m_spawnedEnemiesLoop].transform.position, Quaternion.identity);
            m_spawnedEnemies.Add(enemySpawned);
        }

        GameManager.Instance.m_winScreen.SetActive(false);
        m_buttonSpawned = false;
    }

    public void RemoveFromList(GameObject enemy)
    {
        m_spawnedEnemies.Remove(enemy);
    }

    public void WaveCounter()
    {
        m_waveCounter.text = "Wave : " + GameManager.Instance.m_currentWave;
    }
}
