using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    public Button teleportButton;
    public Vector3 teleportPosition1; // 1. teleport noktasý (Inspector'dan ayarlayýn)
    public Vector3 teleportPosition2; // 2. teleport noktasý (Inspector'dan ayarlayýn)
    public AudioClip teleportSound; // Teleportasyon sesi

    private bool canTeleport = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource; // AudioSource komponenti

    // Teleport alanlarý için etiketler
    private string currentTeleportArea = "";

    void Start()
    {
        // SpriteRenderer bileþenini al
        spriteRenderer = GetComponent<SpriteRenderer>();

        // AudioSource komponentini ekleyin
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Eðer yoksa ekle
        }

        // Eðer teleport butonu atanmadýysa, hata vermemesi için kontrol ekleyelim
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(Teleport);
            teleportButton.interactable = false;
        }
        else
        {
            Debug.LogError("Teleport butonu atanmamýþ! Lütfen Inspector'dan butonu baðlayýn.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TeleportArea1"))
        {
            currentTeleportArea = "Area1"; // 1. teleport alanýna girdik
            canTeleport = true;
            teleportButton.interactable = true;
        }
        else if (other.CompareTag("TeleportArea2"))
        {
            currentTeleportArea = "Area2"; // 2. teleport alanýna girdik
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
            // Hangi teleport alanýna girdiðimizi kontrol et
            if (currentTeleportArea == "Area1")
            {
                // 1. teleport alaný için hedef pozisyon
                transform.position = teleportPosition1;
            }
            else if (currentTeleportArea == "Area2")
            {
                // 2. teleport alaný için hedef pozisyon
                transform.position = teleportPosition2;
            }

            // Teleportasyon sesi çal
            if (audioSource != null && teleportSound != null)
            {
                audioSource.PlayOneShot(teleportSound); // Teleportasyon sesi çal
            }

            // Sprite görünür yap
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

            gameObject.SetActive(true);

            Debug.Log("Yeni Pozisyon: " + transform.position);
        }
    }
}
