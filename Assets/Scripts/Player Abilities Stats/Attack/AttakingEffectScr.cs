using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakingEffectScr : MonoBehaviour
{



    public void TurnOffThisGo()// this method is used on each player Attacking amins.
    {
        this.gameObject.SetActive(false);
    }
}
