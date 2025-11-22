using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public Transform swordHolder; 
    public int mergeCount = 6; 

    public GameObject[] swordPrefabs; 

    private List<GameObject>[] storedSwords;

    void Start()
    {
        int maxLevel = swordPrefabs.Length;
        storedSwords = new List<GameObject>[maxLevel];

        for (int i = 0; i < maxLevel; i++)
        {
            storedSwords[i] = new List<GameObject>();
        }
    }

    public void AddSword(int level)
    {
        if (level >= swordPrefabs.Length)
        {
            Debug.Log("LEVEL MAX!");
            return;
        }

        GameObject newSword = Instantiate(swordPrefabs[level]);
        newSword.transform.SetParent(swordHolder);
        newSword.transform.localPosition = Vector3.zero;
        newSword.transform.localRotation = Quaternion.identity;

        storedSwords[level].Add(newSword);
        Debug.Log($"Get Lv{level + 1} Blade. Count total of same level: {storedSwords[level].Count}");

        CheckMerge(level);
    }

    void CheckMerge(int level)
    {
        if (level >= swordPrefabs.Length - 1) return;

        List<GameObject> currentPool = storedSwords[level];

        if (currentPool.Count >= mergeCount)
        {
            Debug.Log($"Lv{level + 1} got {mergeCount} blades, MERGE!");

            for (int i = 0; i < mergeCount; i++)
            {
                GameObject swordToRemove = currentPool[0];
                currentPool.RemoveAt(0); 
                Destroy(swordToRemove);
            }
            AddSword(level + 1);
        }
    }
}
