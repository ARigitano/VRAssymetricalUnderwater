using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// Script attached to the prefab of a torpedo.
    /// </summary>
    public class Torpedo : MonoBehaviour
    {
        /// <summary>
        /// Moving speed of the torpedo.
        /// </summary>
        private float _speed = 30f;
        /// <summary>
        /// Life time of the torpedo before being destroyed.
        /// </summary>
        private float _lifetime = 3f;
        /// <summary>
        /// Prefab for explosion effect.
        /// </summary>
        [SerializeField]
        private GameObject _fireDamage;
        /// <summary>
        /// Point where the explosion prefab spawns.
        /// </summary>
        [SerializeField]
        private Transform _explosionPoint;
        /// <summary>
        /// The entity that sent the torpedo.
        /// </summary>
        public GameObject sender;

        private void OnEnable()
        {
            Invoke("Hide", _lifetime);
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);
        }

        /// <summary>
        /// Deactivates the torpedo.
        /// </summary>
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != sender)
            {
                if (other.CompareTag("Enemy"))
                {
                    Hide();
                    Destroy(other.gameObject);
                }
                else if (other.CompareTag("Player"))
                {
                    other.GetComponent<Ship>().TakeDamage(15);
                    GameObject explosion = (GameObject)Instantiate(_fireDamage, _explosionPoint.position, Quaternion.identity);
                    explosion.transform.parent = other.transform;
                    Destroy(explosion, 5f);
                    Hide();
                }
            }
        }
    }
}
