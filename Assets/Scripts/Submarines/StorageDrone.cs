using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// A drone that brings back a chest if it encounters a shipwreck on its way.
    /// </summary>
    public class StorageDrone : MonoBehaviour
    {
        /// <summary>
        /// Moving speed of the drone.
        /// </summary>
        [SerializeField]
        private float _speed = 10f;
        /// <summary>
        /// Rotation speed of the drone.
        /// </summary>
        [SerializeField]
        private float _speedRotation = 5f;
        /// <summary>
        /// Life time of the drone before coming back to the ship.
        /// </summary>
        [SerializeField]
        private float _lifetime = 5f;
        /// <summary>
        /// Is the drone back in the ship?
        /// </summary>
        private bool isBack = false;
        /// <summary>
        /// The ship that sent the drone.
        /// </summary>
        public StorageShip sender;
        /// <summary>
        /// Model of a ship to be displayed under the drone.
        /// </summary>
        [SerializeField]
        private GameObject _chest;
        /// <summary>
        /// Has the drone found a chest?
        /// </summary>
        private bool _isChest = false;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ComeBack());
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);

            RaycastHit hit;
            if (!_isChest)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity) && hit.collider.CompareTag("Shipwreck"))
                {
                    _isChest = true;
                    isBack = true;
                    _chest.SetActive(true);
                }
            }

            if (isBack)
            {
                Vector3 dir = sender.transform.position - transform.position;
                dir.y = 0f;
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, _speedRotation * Time.deltaTime);
            }
        }

        /// <summary>
        /// Tells the drone that it's time to come back to the ship.
        /// </summary>
        /// <returns>The delay after which the drone must come back.</returns>
        IEnumerator ComeBack()
        {
            yield return new WaitForSeconds(_lifetime);

            isBack = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject == sender.gameObject && isBack)
            {
                sender.DroneBack();
                Destroy(gameObject);
            }
        }
    }
}
