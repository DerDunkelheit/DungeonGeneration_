using UnityEngine;

namespace Objects.PickUpableObjects
{
    public abstract class PickupObjectBase : MonoBehaviour
    {


        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player");
            }
        }
    }
}
