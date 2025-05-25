using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Scene de�i�tirme i�in gerekli

public class PlayerNameManager : MonoBehaviour
{
    public GameObject nameInputPanel;             // Paneli buraya at�yorsun
    public TMP_InputField nameInputField;         // InputField buraya atanacak
    public TextMeshProUGUI warningText;            // Hata mesaj� i�in TextMeshPro UGUI

    void Start()
    {
        // E�er PlayerPrefs'te isim yoksa, ismi girmesi i�in paneli a��yoruz
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
        string playerNameInput = nameInputField.text.Trim(); // Ad� al ve ba�taki/sondaki bo�luklar� temizle

        // E�er oyuncu ad� 3 karakterden k�sa ise, hata mesaj� g�ster
        if (playerNameInput.Length < 3)
        {
            warningText.text = "L�tfen en az 3 karakter girin!"; // Hata mesaj�n� g�ster
            warningText.gameObject.SetActive(true); // Mesaj� aktif et
            return; // K�sa ismi kabul etme
        }

        // Ad� PlayerPrefs'e kaydet
        PlayerPrefs.SetString("PlayerName", playerNameInput);
        PlayerPrefs.Save();

        // Paneli gizle
        nameInputPanel.SetActive(false);
        warningText.gameObject.SetActive(false); // Hata mesaj�n� gizle
        Debug.Log("Player name saved: " + playerNameInput);

        // Ana men�ye ge�i�
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        // Sahne de�i�tirme i�lemi: Ana men� sahnesine ge�i�
        SceneManager.LoadScene("MainMenuScene");
    }
}
