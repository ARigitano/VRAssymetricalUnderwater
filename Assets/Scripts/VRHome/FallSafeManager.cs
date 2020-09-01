using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    /// <summary>
    /// Manages all the items protected by the FailSafe script.
    /// </summary>
    public class FallSafeManager : MonoBehaviour
    {
        /// <summary>
        /// Height under which the items shouldn't fall.
        /// </summary>
        private float _minY = 0.05f;
        /// <summary>
        /// Array containing every protected item.
        /// </summary>
        [SerializeField]
        private FallSafe[] _protectedObjects;

        // Start is called before the first frame update
        void Start()
        {
            _protectedObjects = GameObject.FindObjectsOfType<FallSafe>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (FallSafe protectedObject in _protectedObjects)
            {
                if (protectedObject.gameObject.transform.position.y <= _minY)
                {
                    protectedObject.gameObject.transform.position = protectedObject.initialPosition;
                }
            }
        }
    }
}
