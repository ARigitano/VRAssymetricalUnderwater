using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.InteractableSnapZone;
using Underwater.Submarines;

namespace Underwater.VRHome
{
    /// <summary>
    /// Script to load torpedoes from the VR home to the non VR ship.
    /// </summary>
    public class LoadTorpedo : MonoBehaviour
    {
        /// <summary>
        /// Has a torpedo been placed on the launching pad?
        /// </summary>
        public bool isTorpedo = false;
        /// <summary>
        /// VRTK Snapzone for the torpedoes on the launching bay.
        /// </summary>
        [SerializeField]
        private SnapZoneFacade _snapZone;

        /// <summary>
        /// Tells that a torpedo has been placed on the launching bay.
        /// </summary>
        public void TorpedoSnapped()
        {
            isTorpedo = true;
        }

        /// <summary>
        /// Loads a torpedo from the launching bay to the non VR ship.
        /// </summary>
        public void TorpedoLoaded()
        {
            if (isTorpedo && ShipManager.Instance.torpedo < ShipManager.Instance.torpedoMax)
            {
                ShipManager.Instance.torpedo++;
                GameObject snappedTorpedo = _snapZone.SnappedGameObject;
                _snapZone.Unsnap();
                Destroy(snappedTorpedo);
                isTorpedo = false;
            }
        }
    }
}
