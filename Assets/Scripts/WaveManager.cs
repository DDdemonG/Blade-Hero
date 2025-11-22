using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float gameTime = 0f; 
    public Transform playerTransform; 

    public GameObject[] lootPrefabs; 
    public GameObject enemyPrefab;

    private float spawnTimer = 0f;
    private float spawnInterval = 2f;

    void Update()
    {
        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        float currentInterval = Mathf.Max(0.5f, 2f - (gameTime / 180f));

        if (spawnTimer >= currentInterval)
        {
            SpawnLootOrEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnLootOrEnemy()
    {
        if (Random.value > 0.7f)
        {
            SpawnEnemy();
        }
        else
        {
            SpawnLoot();
        }
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
}
