using System.Collections.Generic;
using UnityEngine;

public class HeartContainerManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform container;

    private List<HeartSlot> slots = new List<HeartSlot>();
    private const float HP_PER_HEART = 10f;

    public void InitHearts(int maxHealth)
    {
        foreach (Transform child in container) Destroy(child.gameObject);
        slots.Clear();

        int amount = Mathf.CeilToInt(maxHealth / HP_PER_HEART);

        for (int i = 0; i < amount; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, container);
            HeartSlot slot = newHeart.GetComponent<HeartSlot>();
            slots.Add(slot);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateState(currentHealth, i);
        }
    }
}
