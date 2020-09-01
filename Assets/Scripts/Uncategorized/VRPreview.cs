using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater
{
    /// <summary>
    /// Hides the VR view on the monitor.
    /// </summary>
    public class VRPreview : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.XR.XRSettings.showDeviceView = false;
        }
    }
}
