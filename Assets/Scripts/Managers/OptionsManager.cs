using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;

public class OptionsManager : MonoBehaviour
{
    private AudioManager m_audioManager;
    public bool m_muteAudio = false;

    public List<TMP_FontAsset> m_fontList;
    public static event Action m_fontUpdated;

    void Start()
    {
        m_audioManager = GameManager.Instance.m_audioManager;
    }

    public TMP_FontAsset GetFontClass(string classID)
    {
        switch(classID)
        {
            case "MenuText":
                return m_fontList[0];

            case "CardTitle":
                return m_fontList[1];

            case "CardBody":
                return m_fontList[2];

            case "CardBodyBold":
                return m_fontList[3];

            case "MenuTextBold":
                return m_fontList[4];

            default:
                return m_fontList[0];
        }
    } 

    public void UpdateFont()
    {
        m_fontUpdated?.Invoke();
    }
}
