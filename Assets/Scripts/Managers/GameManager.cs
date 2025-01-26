using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public OptionsManager m_optionsManager { get; private set; }
    public AudioManager m_audioManager { get; private set; }
    public DeckManager m_deckManager { get; private set; }
    public UIManager m_uIManager { get; private set; }
    public LootManager m_lootManager { get; private set; }

    private ShopManager m_shopManager;
    private DrawPileManager m_drawPileManager;
    private EnemyBase m_enemyBase;

    public WaveManager m_waveManager;
    public Player m_player;

    public bool m_playerTurn = true;
    public bool m_managersSet;

    public int m_currentWave;

    public GameObject m_winScreen;
    public GameObject m_loseScreen;
    public GameObject m_gameScreen;

    #region Initialize
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializaManagers();
        }
        else
        {
            Destroy(gameObject);
        }
        
        m_player = FindFirstObjectByType<Player>();
        m_winScreen.SetActive(false);
        m_loseScreen.SetActive(false);
    }

    private void InitializaManagers()
    {
        m_optionsManager = GetComponentInChildren<OptionsManager>();
        m_audioManager = GetComponentInChildren<AudioManager>();
        m_deckManager = GetComponentInChildren<DeckManager>();
        m_uIManager = GetComponentInChildren<UIManager>();
        m_lootManager = GetComponentInChildren<LootManager>();

        m_drawPileManager = FindFirstObjectByType<DrawPileManager>();
        m_waveManager = FindFirstObjectByType<WaveManager>();
        if (m_optionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if(prefab == null)
            {
                Debug.Log($"OptionsManger prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                m_optionsManager = GetComponentInChildren<OptionsManager>();
            }
        }

        if (m_audioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManger prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                m_audioManager = GetComponentInChildren<AudioManager>();
            }
        }

        if (m_deckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManger prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                m_deckManager = GetComponentInChildren<DeckManager>();
            }
        }

        m_optionsManager.UpdateFont();
    }
    #endregion

    #region Turns
    public void EndTurn()
    { 
        m_player.DebuffsActivate();
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        for (int i = 0; i < m_waveManager.m_spawnedEnemies.Count; i++)
        {
            m_enemyBase = m_waveManager.m_spawnedEnemies.ElementAt(i).GetComponent<EnemyBase>();
            m_enemyBase.DebuffsActivate();
            m_enemyBase.UseAbility();
            m_enemyBase.AbilityToUse();
            yield return new WaitForSeconds(1);
        }
        m_player.StartPlayerTurn();
    }
    #endregion
    public void ResetWorld()
    {
        m_drawPileManager.ResetDeck();
        m_drawPileManager.MakeDrawPile(m_drawPileManager.m_starterDeck);
        m_player.ResetPlayerValues();
        m_currentWave = 0;
        m_loseScreen.SetActive(false);
        
    }
}
