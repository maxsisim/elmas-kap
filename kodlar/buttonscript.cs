using UnityEngine;
using UnityEngine.UI;

public class OpenURLButton : MonoBehaviour
{
    public Button openLinkButton;
    public string url = "https://github.com/maxsisim/elmas-kap"; // Gidilecek link

    void Start()
    {
        openLinkButton.onClick.AddListener(OpenLink);
    }

    void OpenLink()
    {
        Application.OpenURL(url);
    }
}
