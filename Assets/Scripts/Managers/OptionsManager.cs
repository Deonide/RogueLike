using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private AudioManager m_audioManager;
    public bool m_muteAudio = false;

    void Start()
    {
        m_audioManager = GameManager.Instance.m_audioManager;
    }


    void Update()
    {
        
    }
}
