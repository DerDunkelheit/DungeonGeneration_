using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance;

    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite foodSprite;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
}
