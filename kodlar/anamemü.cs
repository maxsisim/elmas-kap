using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Ana oyun modu başlat
    public void PlayGame()
    {
        SceneToLoadHolder.sceneName = "MainGameScene";
        SceneManager.LoadScene("LoadingScene");
    }

    // Fight Club modu başlat
    public void PlayFightClub()
    {
        SceneToLoadHolder.sceneName = "FightClubScene";
        SceneManager.LoadScene("LoadingScene");
    }

    // Seçenekler sahnesine git
    public void ShowOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    // Credits sahnesine git
    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Credits ya da Options’tan ana menüye dönüş
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Oyunu kapat
    public void QuitGame()
    {
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();
    }

    // Oyun içi duraklatma (pause/resume)
    public void TogglePauseMenu(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // Oyun devam etsin
            // Duraklatma menüsünü gizle
        }
        else
        {
            Time.timeScale = 0f; // Oyun duraklansın
            // Duraklatma menüsünü aç
        }
    }

    // Skor tablosu menüsünü göster
    public void ShowScorePanel()
    {
        SceneManager.LoadScene("ScorePanelScene"); // ScorePanelScene, skorları gösteren sahnenin adı
    }

    // Yeni sahne yüklendikten sonra Time.timeScale'ı düzelt
    void OnEnable()
    {
        Time.timeScale = 1f; // Yeni sahnede TimeScale'ı 1 yap
    }
}
