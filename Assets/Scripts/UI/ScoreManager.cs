using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    public int kills = 0;
    public int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddKill(int scoreReward)
    {
        kills++;
        score += scoreReward;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "SCORE: " + score.ToString();

        if (finalScoreText != null) finalScoreText.text = "TOTAL KILLS: " + kills.ToString() + "\n" + "TOTAL SCORE: " + score.ToString();
    }
}
