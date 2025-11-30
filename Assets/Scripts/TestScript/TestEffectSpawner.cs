using UnityEngine;

public class TestEffectSpawner : MonoBehaviour
{
    public GameObject[] effectPrefabs;

    public float interval = 3f;
    public bool isLooping = true;

    public KeyCode triggerKey = KeyCode.T; 

    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            SpawnNextEffect();
        }

        if (isLooping)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                SpawnNextEffect();
                timer = 0f;
            }
        }
    }

    void SpawnNextEffect()
    {
        if (effectPrefabs == null || effectPrefabs.Length == 0)
        {
            Debug.LogWarning("empty list");
            return;
        }

        GameObject prefabToSpawn = effectPrefabs[currentIndex];

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log($"testing {currentIndex + 1} : {prefabToSpawn.name}");
        }

        currentIndex++;

        if (currentIndex >= effectPrefabs.Length)
        {
            currentIndex = 0;
            Debug.Log("------------ LOOP ------------");
        }
    }
}
