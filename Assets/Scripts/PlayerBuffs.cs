using UnityEngine;
using System.Collections;

public class PlayerBuffs : MonoBehaviour
{
    private PlayerController moveScript;
    private SwordManager swordManager;
    private SwordFormation swordFormation;

    void Awake()
    {
        moveScript = GetComponent<PlayerController>();
        swordManager = GetComponent<SwordManager>();
        swordFormation = GetComponentInChildren<SwordFormation>();
    }

    public void ApplyBuff(BuffItem.BuffType type, float amount, float duration)
    {
        switch (type)
        {
            case BuffItem.BuffType.SpeedUp:
                StartCoroutine(SpeedUp(amount, duration));
                break;

            case BuffItem.BuffType.Shield:
                PlayerHealth health = GetComponent<PlayerHealth>();
                if (health != null) health.AddShield();
                break;

            case BuffItem.BuffType.SpinSpeed:
                StartCoroutine(SpinSpeedRoutine(amount, duration));
                break;
        }
    }

    IEnumerator SpinSpeedRoutine(float multiplier, float time)
    {
        if (swordFormation == null) yield break;

        float originalSpeed = swordFormation.rotateSpeed;

        swordFormation.rotateSpeed *= multiplier;
        Debug.Log("Spin Speed UUUP!!");

        yield return new WaitForSeconds(time);

        swordFormation.rotateSpeed = originalSpeed;
        Debug.Log("Spin speed normal.");
    }

    IEnumerator SpeedUp(float multiplier, float time)
    {
        if (moveScript == null) yield break;

        float originalSpeed = moveScript.moveSpeed;

        moveScript.moveSpeed *= multiplier;
        Debug.Log(" Speed UUUP!!");

        yield return new WaitForSeconds(time);

        moveScript.moveSpeed = originalSpeed;
        Debug.Log(" speed normal.");
    }

}