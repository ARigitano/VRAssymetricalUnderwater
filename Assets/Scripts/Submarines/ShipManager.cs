using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Underwater.VRHome;
using Underwater.UI;

namespace Underwater.Submarines
{
    /// <summary>
    /// Manages the non VR ship.
    /// </summary>
    public class ShipManager : MonoBehaviour
    {
        #region Singleton

        private static ShipManager _instance;
        public static ShipManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("ShipManager is NULL");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion

        /// <summary>
        /// List of ships currently controllable by the non VR player.
        /// </summary>
        [SerializeField]
        private List<GameObject> _shipsAvailable;
        /// <summary>
        /// Reference to the selected ship.
        /// </summary>
        [SerializeField]
        private GameObject _ship;
        /// <summary>
        /// Script of the selected ship.
        /// </summary>
        [SerializeField]
        private Ship _selectedShip;
        /// <summary>
        /// Tracks the position of the selected ship in the list of ships.
        /// </summary>
        [SerializeField]
        private int _shipId = 0;
        /// <summary>
        /// Moving speed of the selected ship.
        /// </summary>
        public float _speed;
        /// <summary>
        /// Rotation speed of the selected ship.
        /// </summary>
        private float _speedRotation;
        /// <summary>
        /// Are the external lights of the selected ship on?
        /// </summary>
        public bool lightsOn = true;
        /// <summary>
        /// Reference to the rigidbody of the selected ship.
        /// </summary>
        [SerializeField]
        private Rigidbody _rigidbody;
        /// <summary>
        /// Health of the selected ship.
        /// </summary>
        private int _hp;
        /// <summary>
        /// Max health of the selected ship.
        /// </summary>
        private int _hpMax;
        /// <summary>
        /// Number of loaded torpedoes in the selected ship.
        /// </summary>
        public int torpedo = 0;
        /// <summary>
        /// Max number of torpedoes that can be loaded in the selected ship.
        /// </summary>
        public int torpedoMax = 3;
        /// <summary>
        /// Can the selected ship move?
        /// </summary>
        private bool _canMove = true;
        /// <summary>
        /// Portrait for the non VR player during dialogs with NPCs.
        /// </summary>
        public Sprite portrait;
        /// <summary>
        /// Name for the non VR player during dialogs with NPCs.
        /// </summary>
        public string nameShip;
        /// <summary>
        /// Is the selected ship currently in a difficult turn area?
        /// </summary>
        public bool difficultTurn = false;
        /// <summary>
        /// A booster for the movement speed of the selected ship.
        /// </summary>
        private bool _booster = false;
        /// <summary>
        /// The speed modification brounght by the booster.
        /// </summary>
        private float _boostedSpeed = 1f;
        /// <summary>
        /// Reference to the ship camera script.
        /// </summary>
        [SerializeField]
        private MSCameraController _cameraController;
        /// <summary>
        /// Modificator that can be applied to the rotation speed of the selected ship.
        /// </summary>
        private float _turnSpeedCoef = 1f;

        /// <summary>
        /// Forces the ship to stop.
        /// </summary>
        public void StopShip()
        {
            _canMove = false;
        }

        /// <summary>
        /// Ends the forced stop of the ship.
        /// </summary>
        public void UnstopShip()
        {
            _canMove = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeShip();
        }

        /// <summary>
        /// Switches between controllable ships.
        /// </summary>
        private void ChangeShip()
        {
            //Keeping track of the position in the list of ships.
            if(_shipId < _shipsAvailable.Count-1)
            {
                _shipId++;
            }
            else
            {
                _shipId = 0;
            }

            //Turning off previous selected ship.
            if (_ship != null)
            {
                _selectedShip = _ship.GetComponent<Ship>();
                _selectedShip.LightsEnable(false);
                _selectedShip.isSelected = false;
            }
                
            //Assigning the new selected ship.
            _ship = _shipsAvailable[_shipId];
            _rigidbody = _ship.GetComponent<Rigidbody>();

            if(_cameraController != null)
                _cameraController.enabled = false;

            _cameraController = _ship.GetComponent<MSCameraController>();
            _cameraController.enabled = true;

            _selectedShip = _ship.GetComponent<Ship>();

            _selectedShip.LightsEnable(true);
            lightsOn = true;

            //Assigning the ship's variables to local variables and updating the UI accordingly.
            AssignShipData(_selectedShip);
            UpdateUI();
        }

        /// <summary>
        /// Assigns the variables of the ship to local variables.
        /// </summary>
        /// <param name="ship">The selected ship.</param>
        private void AssignShipData(Ship ship)
        {
            ship.isSelected = true;
            _speed = ship.speed;
            _speedRotation = ship.rotationSpeed;
            _hpMax = ship.healthMax;
            torpedoMax = ship.torpedoMax;
            _hp = ship.health;
            torpedo = ship.torpedo;
        }

        /// <summary>
        /// Updates the UI with the new ship's variables.
        /// </summary>
        private void UpdateUI()
        {
            UIOrbit.Instance.UpdateHealthText(_hp);
            UIOrbit.Instance.UpdateHealthMaxText(_hpMax);
            UIOrbit.Instance.UpdateTorpedoText(torpedo);
            UIOrbit.Instance.UpdatetorpedoMaxText(torpedoMax);
        }

        // Update is called once per frame
        void Update()
        {
            //Movement actions
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && _canMove)
            {
                //Go forward.
                _rigidbody.AddForce(_ship.transform.forward * Time.deltaTime * _speed * _boostedSpeed * _turnSpeedCoef);
                _selectedShip.BubblesPlay(true);
            }
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && _canMove)
            {
                //Go backward.
                _rigidbody.AddForce(_ship.transform.forward * Time.deltaTime * _speed * -1 / 2 * _boostedSpeed * _turnSpeedCoef);
                _selectedShip.BubblesPlay(false);
            }
            else
            {
                //Ship not moving.
                _selectedShip.BubblesPlay(false);
            }

            if (_canMove && _ship != null)
            {
                //Ship rotations.
                float turnInput = Input.GetAxis("Horizontal");
                _ship.transform.rotation = Quaternion.Euler(_ship.transform.rotation.eulerAngles + new Vector3(0f, turnInput * _speedRotation * Time.deltaTime, 0f));
            }

            if (difficultTurn && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                //Slowing ship rotation in difficult turns.
                _turnSpeedCoef = 0.5f;
            }
            else
            {
                _turnSpeedCoef = 1f;
            }

            //Non movement actions
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Launching ship primary action.
                _selectedShip.Action();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Ship secondary action
                if (_selectedShip.onSecondaryAction != null)
                    _selectedShip.onSecondaryAction();
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                //Switch ship.
                ChangeShip();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //Booster on/off.
                if (!_booster)
                {
                    _boostedSpeed = 2f;
                    _booster = true;
                }
                else
                {
                    _boostedSpeed = 1f;
                    _booster = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                //Lights on/off.
                if (lightsOn)
                {
                    _selectedShip.LightsEnable(false);
                    lightsOn = false;
                }
                else
                {
                    _selectedShip.LightsEnable(true);
                    lightsOn = true;
                }
            }
        }
    }
}
