using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Scene deðiþtirme için gerekli

public class PlayerNameManager : MonoBehaviour
{
    public GameObject nameInputPanel;             // Paneli buraya atýyorsun
    public TMP_InputField nameInputField;         // InputField buraya atanacak
    public TextMeshProUGUI warningText;            // Hata mesajý için TextMeshPro UGUI

    void Start()
    {
        // Eðer PlayerPrefs'te isim yoksa, ismi girmesi için paneli açýyoruz
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            nameInputPanel.SetActive(true);
        }
        else
        {
            nameInputPanel.SetActive(false);
        }
    }

    public void SubmitName()
    {
        string playerNameInput = nameInputField.text.Trim(); // Adý al ve baþtaki/sondaki boþluklarý temizle

        // Eðer oyuncu adý 3 karakterden kýsa ise, hata mesajý göster
        if (playerNameInput.Length < 3)
        {
            warningText.text = "Lütfen en az 3 karakter girin!"; // Hata mesajýný göster
            warningText.gameObject.SetActive(true); // Mesajý aktif et
            return; // Kýsa ismi kabul etme
        }

        // Adý PlayerPrefs'e kaydet
        PlayerPrefs.SetString("PlayerName", playerNameInput);
        PlayerPrefs.Save();

        // Paneli gizle
        nameInputPanel.SetActive(false);
        warningText.gameObject.SetActive(false); // Hata mesajýný gizle
        Debug.Log("Player name saved: " + playerNameInput);

        // Ana menüye geçiþ
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        // Sahne deðiþtirme iþlemi: Ana menü sahnesine geçiþ
        SceneManager.LoadScene("MainMenuScene");
    }
}
