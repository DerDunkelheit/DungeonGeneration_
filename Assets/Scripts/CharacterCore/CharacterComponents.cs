using UnityEngine;

namespace CharacterCore
{
    public class CharacterComponents : MonoBehaviour
    {
        protected float horizontalInput;
        protected float verticalInput;

        protected CharacterController2D controller;
        protected CharacterMovement characterMovement;
        protected Character character;
        protected Animator anim;

        protected virtual void Start()
        {
            controller = GetComponent<CharacterController2D>();
            characterMovement = GetComponent<CharacterMovement>();
            character = GetComponent<Character>();
            anim = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            HandleAbility();
        }

        /// <summary>
        /// Main method. Here we put the logic of each ability
        /// </summary>
        protected virtual void HandleAbility()
        {
            InternalInput();
            HandleInput();
        }

        /// <summary>
        /// Here we got the necessary input we need to perform our actions
        /// </summary>
        protected virtual void HandleInput()
        {

        }

        /// <summary>
        /// Here get the main input we need to control out character
        /// </summary>
        protected virtual void InternalInput()
        {
            if (character.characterType == Character.CharacterType.Player)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");

            }
        }
    }
}
