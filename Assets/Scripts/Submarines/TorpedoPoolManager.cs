using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.Submarines
{
    /// <summary>
    /// Manages a pool of torpedoes that can be activated either by the player or by NPCs.
    /// </summary>
    public class TorpedoPoolManager : MonoBehaviour
    {
        #region Singleton

        private static TorpedoPoolManager _instance;
        public static TorpedoPoolManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("TorpedoPoolManager is NULL");
                
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion

        /// <summary>
        /// Prefab of a torpedo.
        /// </summary>
        [SerializeField]
        private GameObject _torpedoPrefab;
        /// <summary>
        /// Contains the pool of available torpedoes.
        /// </summary>
        [SerializeField]
        private GameObject _torpedoContainer;
        /// <summary>
        /// List all the available torpedoes.
        /// </summary>
        private List<GameObject> _torpedoPool = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            _torpedoPool = GenerateTorpedo(10);
        }

        /// <summary>
        /// Generates new torpedoes.
        /// </summary>
        /// <param name="nbTorpedo">Number of torpedoes to generate.</param>
        /// <returns>All the torpedoes.</returns>
        private List<GameObject> GenerateTorpedo(int nbTorpedo)
        {
            for(int i = 0; i < nbTorpedo; i++)
            {
                GameObject torpedo = (GameObject)Instantiate(_torpedoPrefab);
                torpedo.transform.parent = _torpedoContainer.transform;
                torpedo.SetActive(false);
                _torpedoPool.Add(torpedo);
            }
            
            return _torpedoPool;
        }

        /// <summary>
        /// Makes a torpedo available by anyone requesting it.
        /// </summary>
        /// <returns>The requested torpedo.</returns>
        public GameObject RequestTorpedo(GameObject sender)
        {
            foreach(var torpedo in _torpedoPool)
            {
                if(!torpedo.activeInHierarchy)
                {
                    torpedo.SetActive(true);
                    torpedo.GetComponent<Torpedo>().sender = sender;
                    return torpedo;
                } 
            }

            GameObject newTorpedo = (GameObject)Instantiate(_torpedoPrefab);
            newTorpedo.transform.parent = _torpedoContainer.transform;
            _torpedoPool.Add(newTorpedo);

            return newTorpedo;
        }
    }
}
