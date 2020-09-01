using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underwater.VRHome
{
    public class Extinguisher : MonoBehaviour
    {
        private bool _isGrabbed = false;
        [SerializeField]
        private ParticleSystem _foam;

        public void Grabbing()
        {
            _isGrabbed = true;
        }

        public void unGrabbing()
        {
            _isGrabbed = false;
        }

        public void Shoot()
        {
            if (_isGrabbed)
                _foam.Play();
        }

        public void StopShoot()
        {
            _foam.Stop();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
