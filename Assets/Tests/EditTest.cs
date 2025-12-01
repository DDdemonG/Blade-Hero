using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class EditTest
{
    [Test]
    public void PlayerPrefabCheckComponentsAndPhysicsSettings()
    {
        string prefabPath = "Assets/Prefabs/Player.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        Assert.IsNotNull(playerPrefab, $"pas de joueur prefab");
        
        Rigidbody2D rb = playerPrefab.GetComponent<Rigidbody2D>();
        CircleCollider2D col = playerPrefab.GetComponent<CircleCollider2D>();

        PlayerController controller = playerPrefab.GetComponent<PlayerController>();
        SwordManager swordManager = playerPrefab.GetComponent<SwordManager>();
        PlayerHealth health = playerPrefab.GetComponent<PlayerHealth>();
        PlayerBuffs buffs = playerPrefab.GetComponent<PlayerBuffs>();

        AudioSource audio = playerPrefab.GetComponent<AudioSource>();
        Animator animator = playerPrefab.GetComponent<Animator>();
        

        Assert.IsNotNull(rb, "Player manque Rigidbody2D");
        Assert.AreEqual(0f, rb.gravityScale, "Player Gravity Scale have to be 0");
        
        Assert.IsNotNull(col, "Player manque CircleCollider2D ");
        Assert.IsFalse(col.isTrigger, "Player CircleCollider2D should not be IsTrigger");

        Assert.IsNotNull(controller, "Player manque PlayerController");
        Assert.IsNotNull(swordManager, "Player manque SwordManager");
        Assert.IsNotNull(health, "Player manque PlayerHealth");
        Assert.IsNotNull(buffs, "Player manque PlayerBuffs");

        Assert.IsNotNull(audio, "Player manque AudioSource");
        Assert.IsNotNull(animator, "Player manque Animator");
    }
}
