using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour
{
    public Button openChestButton;
    public Image diamondUI;
    public TextMeshProUGUI diamondText;

    public AudioClip chestOpenSound;
    private AudioSource audioSource;

    private static int diamondCount = 0;
    private bool isOpened = false;
    private Image buttonImage;
    private bool animationInProgress = false;
    private bool isPlayerNear = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }

        buttonImage = openChestButton.GetComponent<Image>();
        openChestButton.onClick.AddListener(() => OpenChest());
        openChestButton.interactable = false;
        SetButtonTransparency(0.7f);

        diamondCount = 0;
        UpdateDiamondText();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (animationInProgress && anim.GetCurrentAnimatorStateInfo(0).IsName("isChest") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.speed = 0f;
            animationInProgress = false;
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            SetButtonTransparency(1f);
            openChestButton.interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            SetButtonTransparency(0.7f);
            openChestButton.interactable = false;
        }
    }

    public void OpenChest()
    {
        if (!isOpened && isPlayerNear && !animationInProgress)
        {
            if (anim != null)
            {
                anim.SetTrigger("isChest");
            }
            animationInProgress = true;
            isOpened = true;

            SetButtonTransparency(1f);
            openChestButton.interactable = false;

            if (audioSource != null && chestOpenSound != null)
            {
                audioSource.PlayOneShot(chestOpenSound);
            }

            AddDiamondToInventory();
            UpdateDiamondText();
        }
    }

    private void SetButtonTransparency(float alpha)
    {
        if (buttonImage != null)
        {
            Color color = buttonImage.color;
            color.a = alpha;
            buttonImage.color = color;
        }
    }

    private void AddDiamondToInventory()
    {
        diamondCount++;

        if (diamondUI != null)
            diamondUI.enabled = true;

        Debug.Log("💎 Elmas eklendi! Toplam (sandık içi sayaç): " + diamondCount);

        // ShopManager elmas sayacını da arttır
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.AddDiamond(1);
        }
        else
        {
            Debug.LogWarning("ShopManager.Instance bulunamadı.");
        }
    }

    private void UpdateDiamondText()
    {
        if (diamondText != null)
        {
            diamondText.text = (diamondCount == 0) ? "" : diamondCount.ToString();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        diamondCount = 0;
        UpdateDiamondText();
    }

    public void OnGameOver()
    {
        diamondCount = 0;
        UpdateDiamondText();
    }

    public void ResetDiamondCount()
    {
        diamondCount = 0;
        UpdateDiamondText();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
