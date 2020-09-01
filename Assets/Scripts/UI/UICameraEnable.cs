using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.UI
{
    /// <summary>
    /// Manages the second non VR camera dedicated to the UI.
    /// </summary>
    public class UICameraEnable : MonoBehaviour
    {
        /// <summary>
        /// Reference to the second non VR camera.
        /// </summary>
        [SerializeField]
        private GameObject _uiCamera;

        // Start is called before the first frame update
        void Start()
        {
            _uiCamera.SetActive(true);
        }
    }
}
