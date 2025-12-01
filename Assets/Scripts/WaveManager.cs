using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private float difficultyMultiplier = 1f;
    public float easyLootRate = 0.6f;
    public float mediumLootRate = 0.5f;
    public float hardLootRate = 0.5f;

    private float currentLootRate;
    public float gameTime = 0f; 
    public Transform playerTransform; 

    public GameObject[] lootPrefabs; 
    public GameObject enemyPrefab;

    private float spawnTimer = 0f;

    public GameObject[] chestPrefabs;
    public float chestChance = 0.05f;
    void Start()
    {
        ApplyDifficulty();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        float baseInterval = Mathf.Max(0.5f, 2f - (gameTime / 180f));
        float currentInterval = baseInterval * difficultyMultiplier;

        if (spawnTimer >= currentInterval)
        {
            SpawnLootOrEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnLootOrEnemy()
    {
        float roll = Random.value;
        if (roll < chestChance)
        {
            SpawnChest();
        }
        switch (MenuController.currentDifficulty)
        {
            case MenuController.Difficulty.Easy:
                currentLootRate = easyLootRate;  
                break;

            case MenuController.Difficulty.Medium:
                currentLootRate = mediumLootRate; 
                break;

            case MenuController.Difficulty.Hard:
                currentLootRate = hardLootRate;  
                break;
        }
        if (Random.value < currentLootRate)
        {
            SpawnLoot();    
        }
        else
        {
            SpawnEnemy();   
        }

        Debug.Log("LootRate = " + currentLootRate);
    }

    void SpawnChest()
    {
        if (chestPrefabs.Length == 0) return;

        int chestIndex = 0;
        float r = Random.value;

        if (gameTime < 60f)
        {
            chestIndex = 0;
        }
        else if (gameTime < 120f)
        {
            if (r < 0.7f) chestIndex = 0;
            else chestIndex = 1;
        }
        else
        {
            if (r < 0.5f) chestIndex = 0;
            else if (r < 0.8f) chestIndex = 1;
            else if (r < 0.95f) chestIndex = 2;
            else chestIndex = 3;
        }

        Vector2 randomPos = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * 10f;

        Instantiate(chestPrefabs[chestIndex], randomPos, Quaternion.identity);
    }

    void SpawnLoot()
    {
        int levelToSpawn = 0; 
        float r = Random.value;

        if (gameTime < 60f)
        {
            levelToSpawn = 0;
        }
        else if (gameTime < 180f)
        {
            // 50% Lv1, 40% Lv2, 10% Lv3
            if (r < 0.5f) levelToSpawn = 0;
            else if (r < 0.9f) levelToSpawn = 1;
            else levelToSpawn = 2;
        }
        else
        {
            // 30% Lv3, 50% Lv4, 20% Lv5
            if (r < 0.3f) levelToSpawn = 2;
            else if (r < 0.8f) levelToSpawn = 3;
            else levelToSpawn = 4;
        }

        Vector2 randomPos = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * 10f;
        Instantiate(lootPrefabs[levelToSpawn], randomPos, Quaternion.identity);
    }

    void SpawnEnemy()
    {
          Debug.Log("Enemy Spawned");
        Vector2 randomPos = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * 12f;
        GameObject enemyObj = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

        EnemyEquip enemyEquip = enemyObj.GetComponent<EnemyEquip>();

        if (enemyEquip != null)
        {

            int level = 0;
            int count = 1;

            if (gameTime < 60f)
            {
                level = 0;
                count = 1;
            }
            else if (gameTime < 180f) 
            {
                if (Random.value > 0.5f)
                {
                    level = 1; count = 1;
                }
                else
                {
                    level = 0; count = Random.Range(2, 4);
                }
            }
            else
            {
                level = Random.Range(2, 4);
                count = Random.Range(3, 6);
            }

            enemyEquip.SetupWeapon(level, count);
        }
    }
    void ApplyDifficulty()
    {
        switch (MenuController.currentDifficulty)
        {
            case MenuController.Difficulty.Easy:
                difficultyMultiplier = 1.4f;  
                break;

            case MenuController.Difficulty.Medium:
                difficultyMultiplier = 1.0f;
                break;

            case MenuController.Difficulty.Hard:
                difficultyMultiplier = 0.6f;  
                break;
        }

        Debug.Log("Wave Difficulty Multiplier: " + difficultyMultiplier);
    }

}
