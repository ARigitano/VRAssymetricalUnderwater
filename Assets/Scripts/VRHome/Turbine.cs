using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    /// <summary>
    /// Makes the turbine of the VR home rotate.
    /// </summary>
    public class Turbine : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(1000f, 0f, 0f) * Time.deltaTime);
        }
    }
}
