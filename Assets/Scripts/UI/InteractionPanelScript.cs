using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Underwater.UI
{
    /// <summary>
    /// Script for the panel displaying contextualized actions.
    /// </summary>
    public class InteractionPanelScript : MonoBehaviour
    {
        /// <summary>
        /// The instructions to display.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _instructions;
        /// <summary>
        /// The image that shows the key to press for this action.
        /// </summary>
        [SerializeField]
        private GameObject _keyImage;

        /// <summary>
        /// Changes the text of the instructions.
        /// </summary>
        /// <param name="newInstruction">New text to display.</param>
        public void ChangeInstruction(string newInstruction)
        {
            _instructions.text = newInstruction;
        }

        /// <summary>
        /// Toggles the key image on or off.
        /// </summary>
        /// <param name="isToggle">Should the image be on or off?</param>
        public void ToggleImage(bool isToggle)
        {
            _keyImage.SetActive(isToggle);
        }
    }
}
