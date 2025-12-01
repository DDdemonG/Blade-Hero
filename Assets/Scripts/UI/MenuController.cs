using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public static Difficulty currentDifficulty = Difficulty.Medium;
    public static bool autoStartAfterReload = false;
    public GameObject startMenuPanel;
    public GameObject gameMenuPanel;
    public GameObject gameOverText;
    public GameObject DeadMenuPanel;

    public AudioSource mainAudio;
    public WaveManager waveManager;
    public PlayerController playerMove;
    public PlayerHealth playerHealth;

    public RectTransform titleRect;
    public RectTransform buttonRect;
    public RectTransform heartSlot;

    public RectTransform hardButton;
    public RectTransform easyButton;
    public RectTransform mediumButton;

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
        if (DeadMenuPanel != null) DeadMenuPanel.SetActive(false);
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

            Vector2 easyStart = easyButton.anchoredPosition;
            Vector2 mediumStart = mediumButton.anchoredPosition;
            Vector2 hardStart = hardButton.anchoredPosition;

            Vector2 titleEnd = new Vector2(titleStart.x, titleStart.y + 1000f);
            Vector2 btnEnd = new Vector2(btnStart.x, btnStart.y - 1000f);
            Vector2 easyEnd = new Vector2(easyStart.x, easyStart.y - 1000f);
            Vector2 hardEnd = new Vector2(mediumStart.x, mediumStart.y - 1000f);
            Vector2 mediumEnd = new Vector2(hardStart.x, hardStart.y - 1000f);

            float duration = 0.6f;
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                float t = timer / duration;

                t = Mathf.SmoothStep(0f, 1f, t);

                titleRect.anchoredPosition = Vector2.Lerp(titleStart, titleEnd, t);
                buttonRect.anchoredPosition = Vector2.Lerp(btnStart, btnEnd, t);
                hardButton.anchoredPosition = Vector2.Lerp(hardStart, hardEnd, t);
                mediumButton.anchoredPosition = Vector2.Lerp(mediumStart, mediumEnd, t);
                easyButton.anchoredPosition = Vector2.Lerp(easyStart, easyEnd, t);

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

        if (playerHealth != null)
        {
            playerHealth.SetMaxHealth(currentDifficultyHP);
        }

        Debug.Log("GO!!!!  Difficulty HP = " + currentDifficultyHP);
    }
    public void SetEasy()
    {
        currentDifficultyHP = easyEnemyHP;
        currentDifficulty = Difficulty.Easy;
        Debug.Log("Difficulty: EASY");
    }

    public void SetMedium()
    {
        currentDifficultyHP = mediumEnemyHP;
        currentDifficulty = Difficulty.Medium;
        Debug.Log("Difficulty: MEDIUM");
    }

    public void SetHard()
    {
        currentDifficultyHP = hardEnemyHP;
        currentDifficulty = Difficulty.Hard;
        Debug.Log("Difficulty: HARD");
    }

    public int GetCurrentDifficultyHP()
    {
        return currentDifficultyHP;
    }
   
    public void RestartGame()
    {
        Time.timeScale = 1f;
        autoStartAfterReload = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  
    }
    public void ShowDeadMenuPanel()
    {
        if (DeadMenuPanel != null) DeadMenuPanel.SetActive(true);
    }
    public void BackToMenu()
    {

        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
    }
