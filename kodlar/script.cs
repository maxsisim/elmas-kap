using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawnerManager : MonoBehaviour
{
    [System.Serializable]
    public class Spawner
    {
        public Transform spawnPoint;
        public TextMeshProUGUI countdownText;
        [HideInInspector] public float timer = 0f;
    }

    public GameObject enemyPrefab;
    public Spawner[] spawners;

    private float spawnInterval = 15f;
    private System.Random random = new System.Random();

    [Header("Score System")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    void Update()
    {
        foreach (var spawner in spawners)
        {
            spawner.timer += Time.deltaTime;

            float countdown = Mathf.Max(0f, spawnInterval - spawner.timer);
            if (spawner.countdownText != null)
                spawner.countdownText.text = Mathf.CeilToInt(countdown).ToString();

            if (spawner.timer >= spawnInterval)
            {
                spawner.timer = 0f;
                int spawnAmount = random.Next(0, 100) < 15 ? 2 : 1;
                for (int i = 0; i < spawnAmount; i++)
                {
                    Instantiate(enemyPrefab, spawner.spawnPoint.position, Quaternion.identity);
                }
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Skor: " + score;
    }

    public void SaveScoreToPrefs(string playerName)
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        PlayerPrefs.SetString("ScoreName_" + scoreCount, playerName);
        PlayerPrefs.SetInt("ScoreValue_" + scoreCount, score);
        PlayerPrefs.SetInt("ScoreCount", scoreCount + 1);
        PlayerPrefs.Save();
    }

    public void GoToMainMenu()
    {
        // PlayerPrefs'ten oyuncu adını al
        string playerName = PlayerPrefs.GetString("PlayerName", "Bilinmeyen");

        SaveScoreToPrefs(playerName);

        SceneManager.LoadScene("MainMenuScene"); // Ana menü sahne adını buraya yaz
    }
}
