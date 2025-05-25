using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CopyToClipboard : MonoBehaviour
{
    public TextMeshProUGUI textToCopy;
    public Button copyButton;

    void Start()
    {
        copyButton.onClick.AddListener(CopyTextToClipboard);
    }

    void CopyTextToClipboard()
    {
        if (!string.IsNullOrEmpty(textToCopy.text))
        {
            GUIUtility.systemCopyBuffer = textToCopy.text;
            Debug.Log("Metin kopyalandý: " + textToCopy.text);
        }
    }
}
