using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    //Was used for the first prototype. Most of it is now obsolete.

    /// <summary>
    /// Script attached to doors.
    /// </summary>
    public class Door : MonoBehaviour
    {
        /// <summary>
        /// Animator of the door.
        /// </summary>
        [SerializeField]
        private Animator _anim;
        /// <summary>
        /// Is the door already open?
        /// </summary>
        [SerializeField]
        private bool _isOpen = false;
        /// <summary>
        /// Material of the LED displaying the access state of the door.
        /// </summary>
        [SerializeField]
        private Material _accessMaterial;
        /// <summary>
        /// Can the door be opened?
        /// </summary>
        private bool _canOpen = false;
        /// <summary>
        /// Water filling the main room when sinking.
        /// </summary>
        [SerializeField]
        private GameObject _waterMain;
        /// <summary>
        /// Water filling the engine room or the SAS (?) when sinking.
        /// </summary>
        [SerializeField]
        private GameObject _waterEngine;
        /// <summary>
        /// Is the water filling the rooms?
        /// </summary>
        private bool _waterPouring = false;
        /// <summary>
        /// Speed at which the water level is rising.
        /// </summary>
        private float _waterPouringSpeed = 0.06f;
        /// <summary>
        /// Animator for the second door of the SAS.
        /// </summary>
        [SerializeField]
        private Animator _secondDoorAnimator;
        /// <summary>
        /// Level of the water in the room at the moment of opening the SAS door.
        /// </summary>
        private Vector3 _waterInitialPosition;
        //Obsolete?
        private bool _waterSASIncreasing = false;
        //Obsolete?
        private bool _waterSASDecreasing = false;
        /// <summary>
        /// Neutral access material.
        /// </summary>
        [SerializeField]
        private Material _neutral;
        /// <summary>
        /// Restricted access material.
        /// </summary>
        [SerializeField]
        private Material _restricted;
        /// <summary>
        /// Granted access material.
        /// </summary>
        [SerializeField]
        private Material _granted;
        /// <summary>
        /// LED that indicated the access level of the door.
        /// </summary>
        [SerializeField]
        private GameObject _indicator;

        /// <summary>
        /// Opening the SAS
        /// </summary>
        /// <returns>Waits for a few seconds.</returns>
        IEnumerator OpenSAS()
        {
            _waterEngine.SetActive(true);
            _waterInitialPosition = _waterEngine.transform.position;
            while (_waterEngine.transform.position.y < _waterMain.transform.position.y)
            {
                _waterEngine.transform.Translate(transform.up * Time.deltaTime * _waterPouringSpeed);
            }
            _anim.SetBool("Card", true);

            yield return new WaitForSeconds(5f);

            //OpenSecondDoor();

            _anim.SetBool("Close", true);
            _anim.SetBool("Card", false);

            while (_waterEngine.transform.position.y > _waterInitialPosition.y)
            {
                _waterEngine.transform.Translate(transform.up * -1 * Time.deltaTime * _waterPouringSpeed);
            }
            _waterEngine.SetActive(false);
            _secondDoorAnimator.SetBool("Card", true);

        }

        /// <summary>
        /// Access to the door has been granted.
        /// </summary>
        public void AccessDoor()
        {
            gameObject.GetComponent<MeshRenderer>().material = _accessMaterial;
            _canOpen = true;
        }

        /// <summary>
        /// Access key has been inserted in the slot.
        /// </summary>
        public void OpenSlot()
        {
            if (!_isOpen && _canOpen)
            {
                _indicator.GetComponent<MeshRenderer>().material = _granted;
                StartCoroutine(Opening());
            }
            else if (!_canOpen)
            {
                _indicator.GetComponent<MeshRenderer>().material = _restricted;
            }
        }

        /// <summary>
        /// The door is opening.
        /// </summary>
        /// <returns>Waits for a few seconds.</returns>
        IEnumerator Opening()
        {
            yield return new WaitForSeconds(1f);
            _anim.SetBool("Card", true);
            _isOpen = true;
        }

        /// <summary>
        /// Calls the ResettingMaterial coroutine.
        /// </summary>
        public void ResetMaterial()
        {
            StartCoroutine(ResettingMaterial());
        }

        /// <summary>
        /// Access level resets to neutral.
        /// </summary>
        /// <returns>Waits for a few seconds.</returns>
        IEnumerator ResettingMaterial()
        {
            yield return new WaitForSeconds(2f);
            _indicator.GetComponent<MeshRenderer>().material = _neutral;
        }

        private void OnTriggerEnter(Collider other)
        {
            /*if(other.tag == "AccessCard" && !_isOpen && _canOpen)
            {
                Debug.Log("Opening");
                _isOpen = true;
                StartCoroutine(OpenSAS());
            }
            else if (_isOpen)
            {
                _isOpen = false;
                _secondDoorAnimator.SetBool("Card", false);
                _waterEngine.SetActive(true);
                while (_waterEngine.transform.position.y < _waterMain.transform.position.y)
                {
                    _waterEngine.transform.Translate(transform.up * Time.deltaTime * _speed);
                }
                _anim.SetBool("Card", true);

                _secondDoorAnimator.SetBool("Close", true);
            }*/
        }
    }
}
