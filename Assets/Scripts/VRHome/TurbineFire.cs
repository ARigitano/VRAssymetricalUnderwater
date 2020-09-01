using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Underwater.Submarines;

namespace Underwater.VRHome
{
    /// <summary>
    /// Handles the engine room of the VR home taking fire if overheating.
    /// </summary>
    public class TurbineFire : MonoBehaviour
    {
        /// <summary>
        /// Fire effect.
        /// </summary>
        [SerializeField]
        private GameObject _fire;
        /// <summary>
        /// Is the fire being extinguished?
        /// </summary>
        private bool _isFoam = false;
        //private float az = 30f;
        /// <summary>
        /// Reference to the UI button to give permission to open the door to the engine room.
        /// </summary>
        [SerializeField]
        private Button _doorButton;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartFire());
        }

        /// <summary>
        /// Decreases the fire intensity.
        /// </summary>
        /// <returns>Waiting for a few seconds.</returns>
        IEnumerator DecreaseFire()
        {
            foreach (Transform child in _fire.transform)
            {
                child.gameObject.SetActive(false);
                yield return new WaitForSeconds(2f);
            }
            //_shipManager.ChangeDiagnosis("BURNT PAINT but REST is OK", 0f, 1f, 0f);
        }

        /// <summary>
        /// Starts the fire in the engine room.
        /// </summary>
        /// <returns>Waiting time before the engine takes fire.</returns>
        IEnumerator StartFire()
        {
            yield return new WaitForSeconds(120f);
            _fire.GetComponent<ParticleSystem>().Play();
            //_shipManager.ChangeDiagnosis("FIRE in ENGINE ROOM", 1f, 0f, 0f);
            _doorButton.interactable = true;
        }

        /// <summary>
        /// Starts decreasing the fire intensity when the extinguisher is used.
        /// </summary>
        /// <param name="other">Foam from the fire extinguisher.</param>
        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("foam");
            _isFoam = true;
            StartCoroutine(DecreaseFire());
        }
    }
}
