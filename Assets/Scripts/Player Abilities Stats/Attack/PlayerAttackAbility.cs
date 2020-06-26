using CharacterCore;
using UnityEngine;

namespace Player_Abilities_Stats.Attack
{
    public class PlayerAttackAbility : CharacterComponents
    {
        [SerializeField] float damage = 1;

        bool isAttackAnimationPlaying = false;

        CharacterFlip flip;

        public float Damage => damage;

        protected override void Start()
        {
            base.Start();

            flip = this.GetComponent<CharacterFlip>();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void HandleInput()
        {
            if (!isAttackAnimationPlaying)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    anim.SetTrigger("AttackTop");
                    isAttackAnimationPlaying = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(flip.playerCurrentxScale == 1) //this additional if statement fixes the scale problem, when hit box scales with sprite.
                    {                                 //TODO: implement logic with right side player's skins.    
                        anim.SetTrigger("AttackLeft");
                        isAttackAnimationPlaying = true;
                    }
                    //else                              // uncomment this part of code if you want to give ability to player do hit in 4 direction
                    //{
                    //    anim.SetTrigger("AttackRight");
                    //    isAttackAnimationPlaying = true;
                    //}
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (flip.playerCurrentxScale == -1)
                    {
                        anim.SetTrigger("AttackLeft");
                        isAttackAnimationPlaying = true;
                    }
                    //else                              // uncomment this as well
                    //{
                    //    anim.SetTrigger("AttackRight");
                    //    isAttackAnimationPlaying = true;
                    //}
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    anim.SetTrigger("AttackDown");
                    isAttackAnimationPlaying = true;
                }
            }
        }

        public void ResetAttackingBool() // this func is on animation Event on playerAttacking animations
        {
            isAttackAnimationPlaying = false;
        }
    }
}
