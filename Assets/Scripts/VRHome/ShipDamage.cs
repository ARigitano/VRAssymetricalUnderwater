using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.Submarines;

namespace Underwater.VRHome
{
    /// <summary>
    /// Handles the effects on the VR home when it takes damages.
    /// </summary>
    public class ShipDamage : MonoBehaviour
    {
        /// <summary>
        /// Number of damages the home has taken.
        /// </summary>
        public int nbDamage = 0;
        /// <summary>
        /// Water effect slowly dripping.
        /// </summary>
        [SerializeField]
        private ParticleSystem _dripping;
        /// <summary>
        /// Water effect pouring in large quantity.
        /// </summary>
        [SerializeField]
        private ParticleSystem _pouring;
        /// <summary>
        /// Fire effect explosion.
        /// </summary>
        [SerializeField]
        private ParticleSystem _exploding;
        /// <summary>
        /// Water effect puddle.
        /// </summary>
        [SerializeField]
        private GameObject _puddle;
        /// <summary>
        /// Reference to the VR ship.
        /// </summary>
        private GameObject _ship;
        /// <summary>
        /// Has a damage diagnosis been given to the non VR player's UI?
        /// </summary>
        private bool _diagnosisGiven = false;
        /// <summary>
        /// Water plane that rises in the cockpit room.
        /// </summary>
        [SerializeField]
        private GameObject _waterMain;
        /// <summary>
        /// Water plane that rises in the SAS or engine room.
        /// </summary>
        [SerializeField]
        private GameObject _waterEngine;
        /// <summary>
        /// Is water currently pouring in the home?
        /// </summary>
        private bool _waterPouring = false;
        /// <summary>
        /// Speed at which the water level rises in the cockpit.
        /// </summary>
        private float _speed = 0.01f;

        // Start is called before the first frame update
        void Start()
        {
            _ship = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (nbDamage <= 0)
            {
                if (!_diagnosisGiven)
                {

                    _diagnosisGiven = true;
                }
            }
            else
            {
                _diagnosisGiven = false;
            }

            if (nbDamage <= 0)
            {
                if (_dripping.isPlaying)
                {
                    _dripping.Stop();
                    //ShipManager.Instance.ChangeDiagnosis("SLIPPERY FLOOR but REST is OK", 0f, 1f, 0f);
                }
            }
            else if (nbDamage >= 1 && nbDamage <= 6)
            {
                if (_pouring.isPlaying)
                    _pouring.Stop();
                if (!_dripping.isPlaying)
                    _dripping.Play();

                _puddle.SetActive(true);
            }
            else if (nbDamage >= 7 && nbDamage <= 11)
            {
                if (!_pouring.isPlaying)
                {
                    _waterMain.SetActive(true);
                    _pouring.Play();
                    _puddle.SetActive(true);
                }

                if (_dripping.isPlaying)
                    _dripping.Stop();

                _waterMain.transform.Translate(transform.up * Time.deltaTime * _speed);
            }
            else
            {
                if (!_exploding.isPlaying)
                    _exploding.Play();

                //Destroy(_ship, 3f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Hammer")
            {
                nbDamage--;
            }
        }
    }
}
