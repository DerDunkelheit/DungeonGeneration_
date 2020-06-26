using UnityEngine;

namespace Player_Abilities_Stats.Attack
{
    public class AttakingEffectScr : MonoBehaviour
    {



        public void TurnOffThisGo()// this method is used on each player Attacking amins.
        {
            this.gameObject.SetActive(false);
        }
    }
}
