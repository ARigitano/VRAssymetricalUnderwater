using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// A fast ship for fighting and exploring.
    /// </summary>
    public class FighterShip : Ship
    {
        /// <summary>
        /// Shoots a torpedo.
        /// </summary>
        public override void Action()
        {
            base.Action();

            GameObject torpedoNew = TorpedoPoolManager.Instance.RequestTorpedo(this.gameObject);
            torpedoNew.transform.position = cannon.position;
            torpedoNew.transform.rotation = cannon.rotation;
            torpedo--;
        }
    }
}
