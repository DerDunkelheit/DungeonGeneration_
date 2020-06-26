using UnityEngine;

namespace CharacterCore
{
    public class CharacterController2D : MonoBehaviour
    {
        public Vector2 CurrentMovement { get; set; }
        public bool NormalMovement { get; set; }

        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            NormalMovement = true;
        }

        void FixedUpdate()
        {
            if (NormalMovement)
            {
                MoveCharacter();
            }
        }


        private void MoveCharacter()
        {
            Vector2 currentMovePosition = rb.position + CurrentMovement * Time.fixedDeltaTime;
            rb.MovePosition(currentMovePosition);
        }

        public void MovePostition(Vector2 newPosition)
        {
            rb.MovePosition(newPosition);
        }

        public void SetMovement(Vector2 newPosition)
        {
            CurrentMovement = newPosition;
        }
    }
}
