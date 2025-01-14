using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public OptionsManager m_optionsManager { get; private set; }
    public AudioManager m_audioManager { get; private set; }
    public DeckManager m_deckManager { get; private set; }

    private int m_currentWave;


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
    }

    private void InitializaManagers()
    {
        m_optionsManager = GetComponentInChildren<OptionsManager>();
        m_audioManager = GetComponentInChildren<AudioManager>();
        m_deckManager = GetComponentInChildren<DeckManager>();

        if(m_optionsManager == null)
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
    }

}
