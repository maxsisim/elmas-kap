using UnityEngine;
using TMPro;

public class InstallTracker : MonoBehaviour
{
    public TextMeshProUGUI totalInstallsText;
    public TextMeshProUGUI activeUserText;

    void Start()
    {
        // Sadece ilk kez yüklemede çalýþýr
        if (!PlayerPrefs.HasKey("hasInstalled"))
        {
            PlayerPrefs.SetInt("hasInstalled", 1);

            int installs = PlayerPrefs.GetInt("totalInstalls", 0);
            installs += 1;
            PlayerPrefs.SetInt("totalInstalls", installs);
        }

        // Aktif kullanýcý = 1 (sabit)
        activeUserText.text = "Aktif Kullanýcý: 1";

        // Toplam yükleme sayýsýný göster
        int total = PlayerPrefs.GetInt("totalInstalls", 1);
        totalInstallsText.text = "Toplam Yükleme: " + total;
    }
}
