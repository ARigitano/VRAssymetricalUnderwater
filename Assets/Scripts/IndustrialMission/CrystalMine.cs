using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.UI;

namespace Underwater.IndustrialMission
{
    /// <summary>
    /// Script attached to unexploited crystal mines.
    /// </summary>
    public class CrystalMine : MonoBehaviour
    {
        /// <summary>
        /// Script of the quest area.
        /// </summary>
        private NewHorizonsEntrance _horizon;
        /// <summary>
        /// Script displaying contextualized interactions.
        /// </summary>
        private InteractionPanelScript _questUI;
        //private string _originalInstruction;
        /// <summary>
        /// Has this mine been marked by a beacon already?
        /// </summary>
        public bool hasBeacon = false;

        // Start is called before the first frame update
        void Start()
        {
            _horizon = GameObject.FindObjectOfType<NewHorizonsEntrance>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !hasBeacon)
            {
                if (_questUI == null)
                    _questUI = GameObject.FindObjectOfType<InteractionPanelScript>();
                if (_questUI != null)
                    _questUI.ToggleImage(true);

                _horizon.AllowDropping(true, this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_questUI != null)
                    _questUI.ToggleImage(false);

                _horizon.AllowDropping(false, null);
            }
        }
    }
}
