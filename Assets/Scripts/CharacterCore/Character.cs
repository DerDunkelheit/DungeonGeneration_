using UnityEngine;

namespace CharacterCore
{
    public class Character : MonoBehaviour
    {
        public enum CharacterType
        {
            Player,
            AI
        };

        [SerializeField] CharacterType characterTypes = CharacterType.AI;


        public CharacterType characterType => characterTypes;
    }
}
