using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool autoStartAfterReload = false;
    public GameObject startMenuPanel;
    public GameObject gameMenuPanel;
    public GameObject gameOverText;

    public AudioSource mainAudio;
    public WaveManager waveManager;
    public PlayerController playerMove;
    public PlayerHealth playerHealth;

    public RectTransform titleRect;
    public RectTransform buttonRect;
    public RectTransform heartSlot;

    public int easyEnemyHP = 50;
    public int mediumEnemyHP = 100;
    public int hardEnemyHP = 200;

    private int currentDifficultyHP = 100;

    private void Start()
    {
        Time.timeScale = 1f;
        if (autoStartAfterReload)
        {
            autoStartAfterReload = false;
            StartCoroutine(StartGameSequence());
            return;
        }
            if (startMenuPanel != null) startMenuPanel.SetActive(true);
        if (gameMenuPanel != null) gameMenuPanel.SetActive(false);
        if (gameOverText != null) gameOverText.SetActive(false);
        if (mainAudio != null) mainAudio.enabled = false;
        if (waveManager != null) waveManager.enabled = false;
        if (playerMove != null) playerMove.enabled = false;

    }

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        if (titleRect != null && buttonRect != null)
        {
            Vector2 titleStart = titleRect.anchoredPosition;
            Vector2 btnStart = buttonRect.anchoredPosition;

            Vector2 titleEnd = new Vector2(titleStart.x, titleStart.y + 1000f);
            Vector2 btnEnd = new Vector2(btnStart.x, btnStart.y - 1000f);

            float duration = 0.6f;
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                float t = timer / duration;

                t = Mathf.SmoothStep(0f, 2f, t);

                titleRect.anchoredPosition = Vector2.Lerp(titleStart, titleEnd, t);
                buttonRect.anchoredPosition = Vector2.Lerp(btnStart, btnEnd, t);

                yield return null;
            }
        }

        if (startMenuPanel != null) startMenuPanel.SetActive(false);
        if (gameMenuPanel != null) gameMenuPanel.SetActive(true);

        if (heartSlot != null)
        {
            Vector2 heartStart = heartSlot.anchoredPosition;

            Vector2 btnEnd = new Vector2(heartStart.x + 960f, heartStart.y);

            float duration = 0.6f;
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                float t = timer / duration;

                t = Mathf.SmoothStep(0f, 1f, t);

                heartSlot.anchoredPosition = Vector2.Lerp(heartStart, btnEnd, t);

                yield return null;
            }
        }

        if (waveManager != null)
        {
            waveManager.enabled = true;
        }

        if (mainAudio != null)
        {
            mainAudio.enabled = true;
        }

        if (playerMove != null) playerMove.enabled = true;

        Debug.Log("GO!!!!  Difficulty HP = " + currentDifficultyHP);
    }
    public void SetEasy()
    {
        currentDifficultyHP = easyEnemyHP;
        Debug.Log("Difficulty: EASY");
    }

    public void SetMedium()
    {
        currentDifficultyHP = mediumEnemyHP;
        Debug.Log("Difficulty: MEDIUM");
    }

    public void SetHard()
    {
        currentDifficultyHP = hardEnemyHP;
        Debug.Log("Difficulty: HARD");
    }

    public int GetCurrentDifficultyHP()
    {
        return currentDifficultyHP;
    }
    public void ShowGameOver()
    {
        if (gameOverText != null)
            gameOverText.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        autoStartAfterReload = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  
    }
    public void ShowRestart()
    {
        if (startMenuPanel != null)
            startMenuPanel.SetActive(true);
    }
}
