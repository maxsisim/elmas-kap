using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("UI ve Menü Ayarlarý")]
    public GameObject shopMenu;
    public Button openMenuButton;
    public Button closeMenuButton;
    public Button buyItemButton;
    public int itemCost = 6;
    public TextMeshProUGUI playerDiamondsText;

    [Header("Teleport Ayarlarý")]
    public Transform teleportTarget;
    public GameObject blackScreen;
    public AudioSource teleportSound;
    public GameObject player;

    [Header("Teleport Mesaj Ayarlarý")]
    public TextMeshProUGUI messageText;
    public GameObject teleportMenu;

    [Header("Gizlenecek UI Nesneleri (Elle Seçim)")]
    public List<GameObject> uiToHide;

    private int playerDiamonds = 0;
    private Image openMenuImage;
    private TextMeshProUGUI openMenuText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ResetDiamondsToStart();
    }

    private void Start()
    {
        UpdateDiamondUI();
        openMenuImage = openMenuButton.GetComponent<Image>();
        openMenuText = openMenuButton.GetComponentInChildren<TextMeshProUGUI>();

        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);
        buyItemButton.onClick.AddListener(BuyItem);

        shopMenu.SetActive(false);
        buyItemButton.gameObject.SetActive(false);
        buyItemButton.interactable = false;

        openMenuButton.gameObject.SetActive(true);
        openMenuButton.interactable = false;
        openMenuButton.GetComponent<Image>().raycastTarget = false;
        SetButtonTransparency(0.7f);

        if (blackScreen != null)
            blackScreen.SetActive(false);

        if (messageText != null)
            messageText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            openMenuButton.interactable = true;
            SetButtonTransparency(1f);
            openMenuButton.GetComponent<Image>().raycastTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            openMenuButton.interactable = false;
            SetButtonTransparency(0.7f);
            openMenuButton.GetComponent<Image>().raycastTarget = false;

            shopMenu.SetActive(false);
            buyItemButton.gameObject.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        shopMenu.SetActive(true);
        CheckIfCanBuy();
    }

    public void CloseMenu()
    {
        shopMenu.SetActive(false);
    }

    public void CheckIfCanBuy()
    {
        buyItemButton.interactable = playerDiamonds >= itemCost;
        buyItemButton.gameObject.SetActive(true);
    }

    public void BuyItem()
    {
        Debug.Log("BuyItem clicked");
        Debug.Log("Current Diamonds: " + playerDiamonds + " / Cost: " + itemCost);

        if (playerDiamonds >= itemCost)
        {
            playerDiamonds -= itemCost;
            UpdateDiamondUI();
            Debug.Log("Item bought!");

            // Müzik durdur
            GameObject.Find("MusicManager")?.GetComponent<AudioSource>()?.Stop();

            StartCoroutine(TeleportPlayer());
        }
        else
        {
            Debug.Log("Not enough diamonds!");
        }

        CheckIfCanBuy();
    }

    public void AddDiamond(int amount)
    {
        playerDiamonds += amount;
        Debug.Log("Diamond added: " + amount + " | Total: " + playerDiamonds);
        UpdateDiamondUI();
        CheckIfCanBuy(); // EKLENDÝ
    }

    public void ResetDiamondsToStart()
    {
        playerDiamonds = 0;
        UpdateDiamondUI();
    }

    private void UpdateDiamondUI()
    {
        if (playerDiamondsText != null)
        {
            playerDiamondsText.text = playerDiamonds.ToString();
        }
    }

    private void SetButtonTransparency(float alpha)
    {
        if (openMenuImage != null)
        {
            Color c = openMenuImage.color;
            c.a = alpha;
            openMenuImage.color = c;
        }

        if (openMenuText != null)
        {
            Color tc = openMenuText.color;
            tc.a = alpha;
            openMenuText.color = tc;
        }
    }

    private IEnumerator TeleportPlayer()
    {
        HideSelectedUIObjects();

        if (blackScreen != null)
            blackScreen.SetActive(true);

        if (teleportSound != null)
            teleportSound.Play();

        yield return new WaitForSeconds(9f);

        if (player != null && teleportTarget != null)
            player.transform.position = teleportTarget.position;

        if (blackScreen != null)
            blackScreen.SetActive(false);

        if (messageText != null)
        {
            yield return StartCoroutine(DisplayMessage("Yaþasýn sonunda kurtuldum senin sayende teþekkürler."));
        }

        yield return new WaitForSeconds(3f);

        if (teleportMenu != null)
            teleportMenu.SetActive(true);
    }

    private void HideSelectedUIObjects()
    {
        foreach (GameObject uiObj in uiToHide)
        {
            if (uiObj != null)
            {
                uiObj.SetActive(false);
            }
        }
    }

    private IEnumerator DisplayMessage(string message)
    {
        messageText.text = "";
        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
