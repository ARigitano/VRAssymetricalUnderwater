using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Underwater.UI
{
    /// <summary>
    /// Script attached to the UI prefab for a quest.
    /// </summary>
    public class Quest : MonoBehaviour
    {
        /// <summary>
        /// Summary of the quest objectives.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _objectiveText;
        /// <summary>
        /// Text displaying the completion state of the quest.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _objectiveNumber;

        /// <summary>
        /// Updates the quest objectives according to the actions of the player.
        /// </summary>
        /// <param name="newObjective">Updated quest objectives.</param>
        public void UpdateQuest(string newObjective)
        {
            _objectiveText.text = newObjective;
        }

        /// <summary>
        /// Updates the quest completion state according to the actions of the player.
        /// </summary>
        /// <param name="newObjective">Updated quest completion state.</param>
        public void UpdateQuestNumber(string newObjective)
        {
            _objectiveNumber.text = newObjective;
        }
    }
}
