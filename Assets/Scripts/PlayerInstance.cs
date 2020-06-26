using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this Class is used to keepPalayer throug Scenes!!
public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
