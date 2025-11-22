using UnityEngine;

public class EnemyEquip : MonoBehaviour
{
    public Transform swordHolder;

    public GameObject[] weaponPrefabs;

    public void SetupWeapon(int level, int count)
    {
        if (level >= weaponPrefabs.Length) level = weaponPrefabs.Length - 1;
        if (level < 0) level = 0;

        for (int i = 0; i < count; i++)
        {
            GameObject newBlade = Instantiate(weaponPrefabs[level]);

            newBlade.transform.SetParent(swordHolder);
            newBlade.transform.localPosition = Vector3.zero;
            newBlade.transform.localRotation = Quaternion.identity;

            newBlade.tag = "EnemyBlade";
        }
    }
}
