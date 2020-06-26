using System.Collections;
using UnityEngine;

namespace AStart
{
    public class AStarManager : MonoBehaviour
    {
        void Start()
        {
            //  StartCoroutine(WaitBeforeScan());
        }

        public void EnableScan()
        {
            AstarPath.active.Scan();
        }

    

        private IEnumerator WaitBeforeScan()
        {
            yield return new WaitForSeconds(1f);
            AstarPath.active.Scan();
        }
    }
}
