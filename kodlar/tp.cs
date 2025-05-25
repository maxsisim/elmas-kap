using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    public Button teleportButton;
    public Vector3 teleportPosition1; // 1. teleport noktas� (Inspector'dan ayarlay�n)
    public Vector3 teleportPosition2; // 2. teleport noktas� (Inspector'dan ayarlay�n)
    public AudioClip teleportSound; // Teleportasyon sesi

    private bool canTeleport = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource; // AudioSource komponenti

    // Teleport alanlar� i�in etiketler
    private string currentTeleportArea = "";

    void Start()
    {
        // SpriteRenderer bile�enini al
        spriteRenderer = GetComponent<SpriteRenderer>();

        // AudioSource komponentini ekleyin
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // E�er yoksa ekle
        }

        // E�er teleport butonu atanmad�ysa, hata vermemesi i�in kontrol ekleyelim
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(Teleport);
            teleportButton.interactable = false;
        }
        else
        {
            Debug.LogError("Teleport butonu atanmam��! L�tfen Inspector'dan butonu ba�lay�n.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TeleportArea1"))
        {
            currentTeleportArea = "Area1"; // 1. teleport alan�na girdik
            canTeleport = true;
            teleportButton.interactable = true;
        }
        else if (other.CompareTag("TeleportArea2"))
        {
            currentTeleportArea = "Area2"; // 2. teleport alan�na girdik
            canTeleport = true;
            teleportButton.interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TeleportArea1") || other.CompareTag("TeleportArea2"))
        {
            currentTeleportArea = "";
            canTeleport = false;
            teleportButton.interactable = false;
        }
    }

    void Teleport()
    {
        if (canTeleport)
        {
            // Hangi teleport alan�na girdi�imizi kontrol et
            if (currentTeleportArea == "Area1")
            {
                // 1. teleport alan� i�in hedef pozisyon
                transform.position = teleportPosition1;
            }
            else if (currentTeleportArea == "Area2")
            {
                // 2. teleport alan� i�in hedef pozisyon
                transform.position = teleportPosition2;
            }

            // Teleportasyon sesi �al
            if (audioSource != null && teleportSound != null)
            {
                audioSource.PlayOneShot(teleportSound); // Teleportasyon sesi �al
            }

            // Sprite g�r�n�r yap
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

            gameObject.SetActive(true);

            Debug.Log("Yeni Pozisyon: " + transform.position);
        }
    }
}
