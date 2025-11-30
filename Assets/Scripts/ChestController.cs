using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;
    private AudioSource audioSource;

    public GameObject[] lootPrefab;
    public GameObject[] bladePrefab;
    public int ChestLevel = 0;

    public float waitTime = 1.0f; 
    public float fadeDuration = 2.0f;

    private SpriteRenderer renderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }


    void OpenChest()
    {
        isOpened = true;

        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        if (audioSource != null)
        {
            audioSource.Play();  
        }

        SpawnLoot();

        StartCoroutine(FadeAndDestroyRoutine());

        Debug.Log("Chest open");
    }

    private void SpawnLoot()
    {
        float rate = Random.value;

        GameObject itemToSpawn = null;
        int bladeLevelIndex = -1; 

        switch (ChestLevel)
        {
            case 0:
                if (rate < 0.05f) itemToSpawn = lootPrefab[0];
                else if (rate < 0.25f) itemToSpawn = lootPrefab[Random.Range(1, lootPrefab.Length)];
                else
                {
                    itemToSpawn = bladePrefab[0];
                    bladeLevelIndex = 0;
                }
                break;

            case 1:
                if (rate < 0.05f) itemToSpawn = lootPrefab[0];
                else if (rate < 0.25f) itemToSpawn = lootPrefab[Random.Range(1, lootPrefab.Length)];
                else if (rate < 0.85f)
                {
                    itemToSpawn = bladePrefab[1];
                    bladeLevelIndex = 1;
                }
                else
                {
                    itemToSpawn = bladePrefab[2];
                    bladeLevelIndex = 2;
                }
                break;

            case 2:
                if (rate < 0.03f) itemToSpawn = lootPrefab[0];
                else if (rate < 0.25f) itemToSpawn = lootPrefab[Random.Range(1, lootPrefab.Length)];
                else if (rate < 0.85f)
                {
                    itemToSpawn = bladePrefab[2];
                    bladeLevelIndex = 2;
                }
                else
                {
                    itemToSpawn = bladePrefab[3];
                    bladeLevelIndex = 3;
                }
                break;

            case 3:
                if (rate < 0.01f) itemToSpawn = lootPrefab[0];
                else if (rate < 0.30f) itemToSpawn = lootPrefab[Random.Range(1, lootPrefab.Length)];
                else if (rate < 0.80f)
                {
                    itemToSpawn = bladePrefab[3];
                    bladeLevelIndex = 3;
                }
                else
                {
                    itemToSpawn = bladePrefab[4];
                    bladeLevelIndex = 4;
                }
                break;
        }

        if (itemToSpawn != null)
        {
            Vector3 spawnPosCorrecter = new Vector3(0.2f, 0f, 0f);
            GameObject loot = Instantiate(itemToSpawn, transform.position-spawnPosCorrecter, Quaternion.identity);


            if (bladeLevelIndex != -1)
            {
                PickupItem pickup = loot.GetComponent<PickupItem>();
                if (pickup != null)
                {
                    pickup.swordLevel = bladeLevelIndex;
                }
            }
        }
    }

    IEnumerator FadeAndDestroyRoutine()
    {
        yield return new WaitForSeconds(waitTime);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            Color c = renderer.color;
            renderer.color = new Color(c.r, c.g, c.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
