using UnityEngine;

public class InstagramButton : MonoBehaviour
{
    // Instagram kullanýcý adý veya profil URL'si
    public string instagramURL = "https://www.instagram.com/muhammed.enes.k/";

    // Butona týklanýnca çaðrýlacak fonksiyon
    public void OpenInstagramProfile()
    {
        // Instagram profilini aç
        Application.OpenURL(instagramURL);
    }
}
