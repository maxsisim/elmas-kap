using UnityEngine;

public class InfoMenuController : MonoBehaviour
{
    public GameObject infoMenu1;
    public GameObject infoMenu2;

    // Men�leri a�ma fonksiyonlar�
    public void OpenInfoMenu1()
    {
        infoMenu1.SetActive(true);
        infoMenu2.SetActive(false);
    }

    public void OpenInfoMenu2()
    {
        infoMenu2.SetActive(true);
        infoMenu1.SetActive(false);
    }

    // Her iki men�y� kapatma
    public void CloseInfoMenus()
    {
        infoMenu1.SetActive(false);
        infoMenu2.SetActive(false);
    }
}
