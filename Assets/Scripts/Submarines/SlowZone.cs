using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// A zone that slows down the speed of the ship.
    /// </summary>
    public class SlowZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ShipManager.Instance.difficultTurn = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ShipManager.Instance.difficultTurn = false;
            }
        }
    }
}
