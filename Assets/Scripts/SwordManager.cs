using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public Transform swordHolder;  
    public int mergeCount = 6;      

    public GameObject[] swordPrefabs;

    private List<GameObject> currentSwords = new List<GameObject>();
    public int currentLevel = 0;

    public int swordLevel = 0;

    public void AddSword(int levelToAdd)
    {
        if (levelToAdd != currentLevel)
        {
            Debug.Log("Pas le même level");
            return;
        }

        GameObject newSword = Instantiate(swordPrefabs[currentLevel]);

        newSword.transform.SetParent(swordHolder);
        newSword.transform.localPosition = Vector3.zero;
        newSword.transform.localRotation = Quaternion.identity;

        currentSwords.Add(newSword);

        CheckMerge();
    }

    void CheckMerge()
    {
        if (currentLevel >= swordPrefabs.Length - 1)
        {
            return;
        }

        if (currentSwords.Count >= mergeCount)
        {
            foreach (var sword in currentSwords)
            {
                Destroy(sword);
            }
            currentSwords.Clear();

            currentLevel++;

            AddSword(currentLevel);

            Debug.Log("LEVEL UP TO" + (currentLevel + 1));
        }
    }
}
