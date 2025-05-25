using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;  // Ölüm menüsü UI'si
    public TextMeshProUGUI diamondText;  // Elmas sayısı yazısı

    [Header("Sahne Geçiş Ayarları")]
    public int gameplaySceneIndex = 1; // Oyun sahnesinin index'i
    public int mainMenuSceneIndex = 0;  // Ana menü sahnesi
    public int fightClubSceneIndex = 2; // Fight Club sahnesi

    void Start()
    {
        deathMenu.SetActive(false);  // Menü başlangıçta gizli
        Time.timeScale = 1f;  // Zamanı normalleştir
    }

    // Ölüm menüsünü göster
    public void ShowDeathMenu()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;  // Zamanı durdur
    }

    // Oyunu yeniden başlat
    public void RestartGame()
    {
        StartCoroutine(LoadSceneWithDelay(gameplaySceneIndex));
    }

    // Ana menüye dön
    public void GoToMainMenu()
    {
        StartCoroutine(LoadSceneWithDelay(mainMenuSceneIndex));
    }

    // Fight Club sahnesine git
    public void GoToFightClub()
    {
        StartCoroutine(LoadSceneWithDelay(fightClubSceneIndex));
    }

    // Fight Club sahnesinde yeniden dene
    public void RetryGameFightClub()
    {
        StartCoroutine(LoadSceneWithDelay(fightClubSceneIndex));
    }

    // Sahne geçişi öncesi verileri sıfırla ve sahne yükle
    private IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        Time.timeScale = 1f; // Yeni sahneye geçmeden önce zamanı düzelt
        ResetGameData(); // Oyuncu verilerini sıfırla

        // Sıfırlama işleminin bitmesini bekle
        yield return new WaitForEndOfFrame();

        // Sahne geçişi
        SceneManager.LoadScene(sceneIndex);
    }

    // Oyunun başlangıcındaki verileri sıfırla
    private void ResetGameData()
    {
        // UI üzerindeki elmas sayısını sıfırla
        if (diamondText != null)
            diamondText.text = "0";

        // ShopManager varsa onun içindeki elmas verisini de sıfırla
        if (ShopManager.Instance != null)
            ShopManager.Instance.ResetDiamondsToStart();
    }
}
