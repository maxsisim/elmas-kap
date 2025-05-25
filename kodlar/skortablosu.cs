using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreboardManager : MonoBehaviour
{
    public GameObject scoreRowPrefab;
    public Transform contentParent;
    public GameObject menuPanel;
    public Button toggleMenuButton;
    public TMP_InputField passwordInput;
    public Button submitPasswordButton;
    private const string correctPassword = "1234";

    [System.Serializable]
    public class ScoreData
    {
        public string playerName;
        public int playerScore;
    }

    private List<ScoreData> playerScores = new List<ScoreData>();

    void Start()
    {
        menuPanel.SetActive(false);
        toggleMenuButton.onClick.AddListener(ToggleMenu);
        submitPasswordButton.onClick.AddListener(CheckPassword);

        LoadSavedScores();
        LoadScoreboard();
    }

    void LoadSavedScores()
    {
        playerScores.Clear();
        string currentPlayer = PlayerPrefs.GetString("PlayerName", "Unknown");
        int score = PlayerPrefs.GetInt("PlayerScore", 0);

        playerScores.Add(new ScoreData { playerName = currentPlayer, playerScore = score });
    }

    void LoadScoreboard()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var score in playerScores)
        {
            GameObject row = Instantiate(scoreRowPrefab, contentParent);
            TMP_Text nameText = row.transform.Find("nameText").GetComponent<TMP_Text>();
            TMP_Text scoreText = row.transform.Find("scoreText").GetComponent<TMP_Text>();

            nameText.text = score.playerName;
            scoreText.text = score.playerScore.ToString();
        }
    }

    void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            menuPanel.SetActive(false);
            AddAdminTagToPlayers();
        }
        else
        {
            Debug.Log("Yanlýþ þifre!");
        }
    }

    void AddAdminTagToPlayers()
    {
        for (int i = 0; i < playerScores.Count; i++)
        {
            if (!playerScores[i].playerName.Contains("admin"))
            {
                playerScores[i].playerName += " admin";
            }
        }

        LoadScoreboard();
    }
}
