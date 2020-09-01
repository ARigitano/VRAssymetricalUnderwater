using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.UI;
using Underwater.Submarines;

namespace Underwater.IndustrialMission
{
    /// <summary>
    /// NPC that helps the player building the machine, answers lore questions and gives a quest.
    /// </summary>
    public class IndustrialVisitingOffice : NPC
    {
        /// <summary>
        /// Reference to the quest panel.
        /// </summary>
        [SerializeField]
        private GameObject _questObjectives;
        /// <summary>
        /// Reference to the interactions panel.
        /// </summary>
        [SerializeField]
        private GameObject _interactionPanel;
        /// <summary>
        /// Has the quest been completed by the player?
        /// </summary>
        public bool _questCompleted = false;
        /// <summary>
        /// Has the quest been returned to the quest giver after completion?
        /// </summary>
        public bool _questGiven = false;
        /// <summary>
        /// Has the quest been accepted by the player?
        /// </summary>
        public bool questAccepted = false;
        /// <summary>
        /// Reference to the non VR ship.
        /// </summary>
        private GameObject _ship;
        /// <summary>
        /// A particle effect when starting the quest.
        /// </summary>
        [SerializeField]
        private GameObject _particle;
        /// <summary>
        /// The quest panel.
        /// </summary>
        [SerializeField]
        private GameObject _questTitle;
        /// <summary>
        /// The ship talking to the Specialist.
        /// </summary>
        private Ship _talker;
        /// <summary>
        /// Prefab for a droppable beacon.
        /// </summary>
        [SerializeField]
        private GameObject _beaconPrefab;

        /// <summary>
        /// Toggles the quest pannel.
        /// </summary>
        /// <returns>A small delay.</returns>
        IEnumerator ToggleQuestUI()
        {
            _questTitle.SetActive(true);
            _particle.SetActive(true);
            yield return new WaitForSeconds(1f);
            _questObjectives.SetActive(true);
        }

        /// <summary>
        /// Sets the quest to started.
        /// </summary>
        public void StartQuest()
        {
            _talker.questItems.Add(_beaconPrefab);
            _questObjectives.SetActive(true);
            questAccepted = true;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasMet)
            {
                _hasMet = true;
                UIDialogueManager.Instance.OpenNPCCom(GetComponent<VIDE_Assign>(), _portrait, _name);
                _talker = other.GetComponent<Submarines.Ship>();
            }
            else if (other.CompareTag("Player") && _questCompleted && !_questGiven)
            {
                UIDialogueManager.Instance.OpenNPCCom(GetComponent<VIDE_Assign>(), _portrait, _name);
                VIDE_Data.VD.SetNode(18);
            }
        }
    }
}
