using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    /// <summary>
    /// Script attached to storage crates.
    /// </summary>
    public class Crate : MonoBehaviour
    {
        /// <summary>
        /// Opening animation.
        /// </summary>
        [SerializeField]
        private Animation _open;
        /// <summary>
        /// Content that should be hidden when the crate is closed.
        /// </summary>
        [SerializeField]
        private GameObject[] _hiddenContent;

        // Start is called before the first frame update
        void Start()
        {
            DisplayContent(false);
        }

        /// <summary>
        /// Toggles the hidden content.
        /// </summary>
        /// <param name="isActive">Should the content be displayed?</param>
        public void DisplayContent(bool isActive)
        {
            if (_hiddenContent != null)
            {
                foreach (GameObject torpedo in _hiddenContent)
                {
                    if (torpedo != null)
                    {
                        torpedo.SetActive(isActive);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AccessCard"))
            {
                _open.Play();
                DisplayContent(true);
            }
        }
    }
}
