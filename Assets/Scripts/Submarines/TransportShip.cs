using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// A polyvalent ship that can transport the VR player.
    /// </summary>
    public class TransportShip : Ship
    {
        private bool _actionSwitch = false;

        /// <summary>
        /// Executes a task chosen by the VR player among different choices.
        /// </summary>
        public override void Action()
        {
            base.Action();

            if (!_actionSwitch)
            {
                LaunchingTorpedo();
            }
            else
            {
                //Other action?
            }
                
        }

        /// <summary>
        /// Launches a torpedo.
        /// </summary>
        private void LaunchingTorpedo()
        {
            GameObject torpedoNew = TorpedoPoolManager.Instance.RequestTorpedo(this.gameObject);
            torpedoNew.transform.position = cannon.position;
            torpedoNew.transform.rotation = cannon.rotation;
            torpedo--;
        }
    }
}
