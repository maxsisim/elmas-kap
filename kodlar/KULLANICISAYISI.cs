using UnityEngine;
using TMPro;

public class InstallTracker : MonoBehaviour
{
    public TextMeshProUGUI totalInstallsText;
    public TextMeshProUGUI activeUserText;

    void Start()
    {
        // Sadece ilk kez y�klemede �al���r
        if (!PlayerPrefs.HasKey("hasInstalled"))
        {
            PlayerPrefs.SetInt("hasInstalled", 1);

            int installs = PlayerPrefs.GetInt("totalInstalls", 0);
            installs += 1;
            PlayerPrefs.SetInt("totalInstalls", installs);
        }

        // Aktif kullan�c� = 1 (sabit)
        activeUserText.text = "Aktif Kullan�c�: 1";

        // Toplam y�kleme say�s�n� g�ster
        int total = PlayerPrefs.GetInt("totalInstalls", 1);
        totalInstallsText.text = "Toplam Y�kleme: " + total;
    }
}
