using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public OptionsManager m_optionsManager { get; private set; }
    public AudioManager m_audioManager { get; private set; }
    public DeckManager m_deckManager { get; private set; }
    private DrawPileManager m_drawPileManager;
    private Player m_player;
    public bool m_playerTurn = true;
    public bool m_managersSet;

    public int m_currentWave;

    public GameObject m_winScreen;
    public GameObject m_loseScreen;

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
        m_drawPileManager = FindFirstObjectByType<DrawPileManager>();

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

    public void EndTurn()
    {
        m_player.DebuffsActivate();
    }

    public void EnemyTurn()
    {
        m_playerTurn = false;

    }

    public void ResetWorld()
    {
        m_drawPileManager.ResetDeck();
        m_drawPileManager.MakeDrawPile(m_drawPileManager.m_starterDeck);
        m_player.ResetPlayerValues();
        m_currentWave = 0;
    }
}
