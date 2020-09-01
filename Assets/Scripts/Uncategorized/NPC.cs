using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.UI;

namespace Underwater
{
    /// <summary>
    /// Base class for a Non Player Character.
    /// </summary>
    public class NPC : MonoBehaviour
    {
        /// <summary>
        /// Reference to the non VR UI.
        /// </summary>
        protected UIOrbit _uiOrbit;
        /// <summary>
        /// Portrait of the NPC.
        /// </summary>
        [SerializeField]
        protected Sprite _portrait;
        /// <summary>
        /// Name of the NPC.
        /// </summary>
        [SerializeField]
        protected string _name;
        /// <summary>
        /// Measures the progress of the dialog.
        /// </summary>
        protected int _dialogValue = 0;
        /// <summary>
        /// Did the player already talk to the NPC?
        /// </summary>
        protected bool _hasMet = false;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasMet)
            {
                _hasMet = true;
                UIDialogueManager.Instance.OpenNPCCom(GetComponent<VIDE_Assign>(), _portrait, _name);
            }
        }
    }
}
