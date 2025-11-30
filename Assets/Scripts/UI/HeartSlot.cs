using UnityEngine;
using UnityEngine.UI;

public class HeartSlot : MonoBehaviour
{
    public Image heartImage;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private float heartValue = 10f;

    public void UpdateState(float currentHealth, int index)
    {
        float minRange = index * heartValue;

        float remainder = currentHealth - minRange;

        if (remainder >= 9) 
        {
            heartImage.sprite = fullHeart;
        }
        else if (remainder >= 4) 
        {
            heartImage.sprite = halfHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart;
        }
    }
}
