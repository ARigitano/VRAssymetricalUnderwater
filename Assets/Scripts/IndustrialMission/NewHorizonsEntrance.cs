using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.UI;
using Underwater.Submarines;

namespace Underwater.IndustrialMission
{
    /// <summary>
    /// Area script for the quest given in the industrial site.
    /// </summary>
    public class NewHorizonsEntrance : MonoBehaviour
    {
        /// <summary>
        /// Array of the UI elements needed for this quest.
        /// </summary>
        [SerializeField]
        private GameObject[] _questUI;
        /// <summary>
        /// Prefab of the beacon to drop.
        /// </summary>
        [SerializeField]
        private GameObject _beaconPrefab;
        /// <summary>
        /// Point of the ship from which the beacons are dropped.
        /// </summary>
        private Transform _dropPoint;
        /// <summary>
        /// Has a beacon just been dropped?
        /// </summary>
        private bool _hasDropped = false;
        /// <summary>
        /// Are the _questUI elements active?
        /// </summary>
        private bool _isActive = false;
        /// <summary>
        /// Number of mines marked by beacons so far.
        /// </summary>
        private int _minesMarked = 0;
        /// <summary>
        /// Reference to the script of the quest giver.
        /// </summary>
        [SerializeField]
        private IndustrialVisitingOffice _questGiver;
        /// <summary>
        /// Reference to the script attached to the quest panel element of this quest.
        /// </summary>
        [SerializeField]
        private Quest _mineQuest;
        /// <summary>
        /// Is it allowed to drop a beacon?
        /// </summary>
        private bool _canDrop = false;
        /// <summary>
        /// Script of the mine of which the player is in range to drop a beacon.
        /// </summary>
        private CrystalMine _highlightedMine;
        /// <summary>
        /// Animator of the quest action panel.
        /// </summary>
        [SerializeField]
        private Animator _interactionAnim;
        /// <summary>
        /// The ship loaded with quest beacons.
        /// </summary>
        private Ship _loadedShip;

        /// <summary>
        /// Gives permission to drop a beacon near a mine.
        /// </summary>
        /// <param name="canDrop">Is it allowed to drop a beacon?</param>
        /// <param name="mine">Mine near which a beacon would be dropped.</param>
        public void AllowDropping(bool canDrop, CrystalMine mine)
        {
            _canDrop = canDrop;
            _highlightedMine = mine;
        }

        /// <summary>
        /// Dropping a beacon.
        /// </summary>
        /// <returns>Waiting a few seconds.</returns>
        private void DroppingBeacon()
        {
            if (_highlightedMine != null)
            {
                if (!_highlightedMine.hasBeacon && !_hasDropped && _canDrop)
                {
                    Instantiate(_beaconPrefab, _dropPoint.position, Quaternion.identity);
                    _hasDropped = true;
                    _highlightedMine.hasBeacon = true;

                    QuestCompletion();

                    StartCoroutine(Wait());
                }
            }
        }

        /// <summary>
        /// Sets a delay before the player can drop another beacon.
        /// </summary>
        /// <returns>Duration of the delay.</returns>
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            _hasDropped = false;
        }

        /// <summary>
        /// Measures the state of completion of the quest.
        /// </summary>
        private void QuestCompletion()
        {
            _minesMarked++;
            _mineQuest.UpdateQuestNumber(_minesMarked.ToString());
            
            if (_minesMarked >= 5)
            {
                _questGiver._questCompleted = true;
                _mineQuest.UpdateQuest("Objective complete! Report to the Specialist.");
                _interactionAnim.SetTrigger("QuestComplete");
            } 
            else
            {
                _interactionAnim.SetTrigger("QuestObjective");
            }
        }

        /// <summary>
        /// Displays or hides the quest action panel.
        /// </summary>
        /// <param name="isActive">Is the panel active?</param>
        /// <returns></returns>
        private bool ToggleQuestPanel(bool isActive)
        {
            foreach (GameObject ui in _questUI)
            {
                ui.SetActive(isActive);
            }

            return isActive;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PirateBunker.Instance.AddRemoveShip(other.transform);

                if(_questGiver.questAccepted && !_questGiver._questCompleted)
                { 
                    if (_loadedShip == null)
                    {
                        foreach (GameObject questItem in other.GetComponent<Ship>().questItems)
                        {
                            if (questItem == _beaconPrefab)
                            {
                                _loadedShip = other.GetComponent<Ship>();
                                _dropPoint = _loadedShip.dropPoint;
                                _loadedShip.onSecondaryAction = DroppingBeacon;
                                _isActive = ToggleQuestPanel(!_isActive);
                                break;
                            }
                        }
                    }
                    else if (other.gameObject == _loadedShip)
                    {
                        _isActive = ToggleQuestPanel(!_isActive);

                        if (_isActive)
                            _loadedShip.onSecondaryAction = DroppingBeacon;
                        else
                            _loadedShip.onSecondaryAction = null;
                    }
                }
            }
        }
    }
}
