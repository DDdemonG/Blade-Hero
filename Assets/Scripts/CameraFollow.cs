using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   
    public float smoothSpeed = 10f;

    public Vector3 offset;     

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
    }
}
