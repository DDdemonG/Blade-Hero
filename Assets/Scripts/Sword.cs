using UnityEngine;

public class Sword : MonoBehaviour
{
    public int level = 0; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Sword otherSword = other.GetComponent<Sword>();

      
        if (otherSword == null) return;

       
        if (!gameObject.activeInHierarchy || !otherSword.gameObject.activeInHierarchy)
            return;

      
        if (level > otherSword.level)
        {
            
            Destroy(otherSword.gameObject);
        }
        else if (level < otherSword.level)
        {
         
            Destroy(gameObject);
        }
        else
        {
           
            Destroy(otherSword.gameObject);
            Destroy(gameObject);
        }
    }
}
