using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchJoystickMover : MonoBehaviour, IPointerDownHandler
{
    [Header("Joystick ve UI Ayarları")]
    public RectTransform joystick;             // Joystick UI objesi
    public Canvas canvas;                      // UI Canvas'ı

    [Header("Kilit Butonu")]
    public Button teleportToggleButton;        // Kilit/Aç butonu
    public Text teleportToggleText;            // Buton yazısı

    private bool teleportMode = false;         // Joystick'in taşınabilirliği
    private Vector2 initialPosition;           // Başlangıçtaki joystick pozisyonu

    void Start()
    {
        // Başlangıçtaki joystick pozisyonunu kaydet
        initialPosition = joystick.anchoredPosition;

        // Kilit butonuna tıklama fonksiyonu ekle
        if (teleportToggleButton != null)
        {
            teleportToggleButton.onClick.AddListener(ToggleTeleportMode);
        }

        UpdateToggleText();
    }

    // Ekranın herhangi bir yerine tıklanırsa
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!teleportMode || joystick == null || canvas == null)
            return;

        Vector2 localPoint;

        // Ekran pozisyonunu canvas içindeki local pozisyona çevir
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        // Joystick'i yeni pozisyona ışınla
        joystick.anchoredPosition = localPoint;
    }

    // Kilit açma/kapama fonksiyonu
    public void ToggleTeleportMode()
    {
        teleportMode = !teleportMode;

        if (!teleportMode)
        {
            // Kilitliyse joystick başlangıç pozisyonuna geri döner
            joystick.anchoredPosition = initialPosition;
        }

        UpdateToggleText();
    }

    // Buton yazısını güncelle
    void UpdateToggleText()
    {
        if (teleportToggleText != null)
        {
            teleportToggleText.text = teleportMode ? "🔓 Joystick Serbest" : "🔒 Joystick Sabit";
        }
    }
}
