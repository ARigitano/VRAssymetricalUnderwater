using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    /// <summary>
    /// Script attached to an item that shouldn't fall under a certain height.
    /// </summary>
    public class FallSafe : MonoBehaviour
    {
        /// <summary>
        /// Position of the item at start.
        /// </summary>
        public Vector3 initialPosition;

        // Start is called before the first frame update
        void Start()
        {
            initialPosition = gameObject.transform.position;
        }
    }
}
