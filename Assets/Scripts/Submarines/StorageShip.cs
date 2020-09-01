using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// A ship filled with chests for carrying items.
    /// </summary>
    public class StorageShip : Ship
    {
        /// <summary>
        /// Is the drone available?
        /// </summary>
        private bool _isDrone = true;

        /// <summary>
        /// Launches a drone that brings back a chest if it encounters a shipwreck on its way.
        /// </summary>
        public override void Action()
        {
            base.Action();

            if (_isDrone)
            {
                GameObject drone = (GameObject)Instantiate(_deployablePrefab, cannon.position, cannon.rotation);
                drone.GetComponent<StorageDrone>().sender = this;
                _isDrone = false;
            }
        }

        /// <summary>
        /// Sets the drone to being back.
        /// </summary>
        public void DroneBack()
        {
            _isDrone = true;
        }
    }
}
