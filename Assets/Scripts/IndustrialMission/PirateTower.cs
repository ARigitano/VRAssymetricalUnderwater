using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.Submarines;

namespace Underwater.IndustrialMission
{
    /// <summary>
    /// A torpedo launching tower for the pirate who attacks the player in the unexploited mines area.
    /// </summary>
    public class PirateTower : MonoBehaviour
    {
        /// <summary>
        /// The tower gameObject.
        /// </summary>
        [SerializeField]
        private GameObject _tower;
        /// <summary>
        /// Gun of the tower.
        /// </summary>
        [SerializeField]
        private GameObject _gun;
        /// <summary>
        /// Reference to the non VR ship.
        /// </summary>
        private Transform _ship;
        /// <summary>
        /// Rotation speed of the tower.
        /// </summary>
        private float _speed = 1f;
        /// <summary>
        /// Point where the torpedoes are spawned.
        /// </summary>
        [SerializeField]
        private Transform _shootingPoint;
        /// <summary>
        /// Does the tower sees the ship?
        /// </summary>
        //public bool seeing = false;
        [SerializeField]
        private float _range = 1.2f;
        /// <summary>
        /// All the player ships that are in range of shooting.
        /// </summary>
        [SerializeField]
        Collider[] enemyInRange;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Shooting());
        }

        /// <summary>
        /// Shoots a torpedo at the ship.
        /// </summary>
        /// <returns></returns>
        IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                if (_ship != null)
                {
                    GameObject torpedoNew = TorpedoPoolManager.Instance.RequestTorpedo(this.gameObject);
                    torpedoNew.transform.position = _shootingPoint.position;
                    torpedoNew.transform.rotation = _tower.transform.rotation;
                }
            }
        }

        /// <summary>
        /// Check if a player ship is in range of shooting and returns it.
        /// </summary>
        /// <returns>The ship returned.</returns>
        private Transform FindingShipRange()
        {
            foreach (Transform ship in PirateBunker.Instance.shipsClose)
            {
                float distance = Vector3.Distance(ship.position, transform.position);

                if (distance <= _range)
                {
                    return ship;
                }
            }

            return null;
        }

        /// <summary>
        /// Targets the ship in range for shooting.
        /// </summary>
        private void TargettingShip()
        {
            Vector3 targetDirection = _ship.position - _tower.transform.position;
            float singleStep = _speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(_tower.transform.forward, targetDirection, singleStep, 0.0f);
            _tower.transform.rotation = Quaternion.LookRotation(newDirection);

            if (ShipManager.Instance.lightsOn)
            {
                _range = 80f;
            }
            else
            {
                _range = 40f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(PirateBunker.Instance.shipsClose.Count == 0)
            {

            }
            else
            {
                _ship = FindingShipRange();
                
                if(_ship != null)
                {
                    TargettingShip();
                }
            }
        }
    }
}
