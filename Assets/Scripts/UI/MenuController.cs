using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject startMenuPanel;

    public AudioSource mainAudio;
    public WaveManager waveManager;
    public PlayerController playerMove;

    public RectTransform titleRect;
    public RectTransform buttonRect;

    private void Start()
    {
        if (startMenuPanel != null) startMenuPanel.SetActive(true);
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

        if (waveManager != null)
        {
            waveManager.enabled = true;
        }

        if (mainAudio != null)
        {
            mainAudio.enabled = true;
        }

        if (playerMove != null) playerMove.enabled = true;

        Debug.Log("GO!!!!");
    }
}
