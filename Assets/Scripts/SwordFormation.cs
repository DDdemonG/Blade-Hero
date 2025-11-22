using UnityEngine;

public class SwordFormation : MonoBehaviour
{
    public float radius = 2f;  
    public float rotateSpeed = 100f;
    public bool pointOutward = true;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        ArrangeSwords();
    }

    void ArrangeSwords()
    {
        int swordCount = transform.childCount;

        if (swordCount == 0) return;

        float angleStep = 360f / swordCount;

        for (int i = 0; i < swordCount; i++)
        {
            Transform sword = transform.GetChild(i);

            float currentAngle = i * angleStep;
            float radian = currentAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;

            sword.localPosition = new Vector3(x, y, 0);

            if (pointOutward)
            {
                sword.localRotation = Quaternion.Euler(0, 0, currentAngle - 270);
            }
            else
            {
                sword.localRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
            }
        }
    }
}
