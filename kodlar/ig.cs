using UnityEngine;

public class InstagramButton : MonoBehaviour
{
    // Instagram kullan�c� ad� veya profil URL'si
    public string instagramURL = "https://www.instagram.com/muhammed.enes.k/";

    // Butona t�klan�nca �a�r�lacak fonksiyon
    public void OpenInstagramProfile()
    {
        // Instagram profilini a�
        Application.OpenURL(instagramURL);
    }
}
