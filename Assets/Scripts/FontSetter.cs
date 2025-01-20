using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class FontSetter : MonoBehaviour
{
    public string m_fontClass;
    
    private void OnEnable()
    {
        //Subscribe to the event.
        OptionsManager.m_fontUpdated += SetFont;
    }

    private void OnDisable()
    {
        //Unsubscribe to prevent memory leaks.
        OptionsManager.m_fontUpdated -= SetFont;
    }

    private void SetFont()
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();

        if (textComponent && GameManager.Instance.m_optionsManager != null)
        {
            textComponent.font = GameManager.Instance.m_optionsManager.GetFontClass(m_fontClass);
        }
    }
}
