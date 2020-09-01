using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.Submarines;
using Underwater.UI;

namespace Underwater.IndustrialMission
{
    /// <summary>
    /// Pirate NPC who attacks the player when entering the unexploited mines area.
    /// </summary>
    public class PirateBunker : NPC
    {
        #region Singleton

        private static PirateBunker _instance;
        public static PirateBunker Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("PirateBunker is NULL");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion

        /// <summary>
        /// List of all the player ships in the area.
        /// </summary>
        public List<Transform> shipsClose = new List<Transform>();

        /// <summary>
        /// Adds or remove a ship from the list.
        /// </summary>
        /// <param name="shipToAddRemove">The ship to add or remove.</param>
        public void AddRemoveShip(Transform shipToAddRemove)
        {
            foreach(Transform ship in shipsClose)
            {
                if(ship == shipToAddRemove)
                {
                    shipsClose.Remove(shipToAddRemove);
                    break;
                } 
            }

            shipsClose.Add(shipToAddRemove);
        }
    }
}
