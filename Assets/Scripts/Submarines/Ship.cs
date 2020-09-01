using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Underwater.UI;

namespace Underwater.Submarines
{
    /// <summary>
    /// Base class for ships controllable by the non VR player.
    /// </summary>
    public class Ship : MonoBehaviour
    {
        /// <summary>
        /// Name of the ship.
        /// </summary>
        public string shipName;
        /// <summary>
        /// Movement speed of the ship.
        /// </summary>
        public float speed;
        /// <summary>
        /// Rotation speed of the ship.
        /// </summary>
        public float rotationSpeed;
        /// <summary>
        /// Maximum health of the ship.
        /// </summary>
        public int healthMax = 100;
        /// <summary>
        /// Maximum number of torpedoes that can be loaded at the same time.
        /// </summary>
        public int torpedoMax = 0;
        /// <summary>
        /// Cargo size the ship can carry.
        /// </summary>
        public int cargoSize = 0;
        /// <summary>
        /// Maximum energy of the ship.
        /// </summary>
        public int energyMax = 100;
        /// <summary>
        /// List of items the ship were given because of quests.
        /// </summary>
        public List<GameObject> questItems;

        /// <summary>
        /// Current health of the ship.
        /// </summary>
        public int health { get; protected set; }
        /// <summary>
        /// Current number of torpedoes loaded.
        /// </summary>
        public int torpedo { get; protected set; }
        /// <summary>
        /// Current cargo transported by the ship.
        /// </summary>
        public int cargo { get; protected set; }
        /// <summary>
        /// Current energy of the ship.
        /// </summary>
        public int energy { get; protected set; }

        /// <summary>
        /// A particle effect of bubbles when the ship is moving.
        /// </summary>
        public ParticleSystem bubbles;
        /// <summary>
        /// Point from which deployables are shot.
        /// </summary>
        public Transform cannon;
        /// <summary>
        /// Point from which items are dropped.
        /// </summary>
        public Transform dropPoint;
        /// <summary>
        /// The front lights of the ship.
        /// </summary>
        public Light lights;
        /// <summary>
        /// Prefab for a deployable.
        /// </summary>
        [SerializeField]
        protected GameObject _deployablePrefab;
        /// <summary>
        /// Is the ship currently controlled?
        /// </summary>
        public bool isSelected = false;

        private void Start()
        {
            health = healthMax;
            torpedo = 0;
            cargo = 0;
            energy = energyMax;
        }

        /// <summary>
        /// Inflicts damages to the ship.
        /// </summary>
        /// <param name="nbDamage">The number of damages inflicted.</param>
        public void TakeDamage(int nbDamage)
        {
            health -= nbDamage;

            if(health <= 0)
            {
                DestroyShip();
            }
            else if(isSelected)
            {
                UpdateHealthUI(health);
            }
        }

        /// <summary>
        /// Destroys the ship.
        /// </summary>
        private void DestroyShip()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Tells the UI how much health to display.
        /// </summary>
        /// <param name="nbHealth">The amount of health to display.</param>
        private void UpdateHealthUI(int nbHealth)
        {
            UIOrbit.Instance.UpdateHealthText(nbHealth);
        }

        /// <summary>
        /// An action that depends of the ship.
        /// </summary>
        public virtual void Action()
        {
            
        }

        /// <summary>
        /// Plays the bubble particle when moving.
        /// </summary>
        /// <param name="isPlaying">Is the effect playing?</param>
        public void BubblesPlay(bool isPlaying)
        {
            if (bubbles != null)
            {
                if (isPlaying)
                    bubbles.Play();
                else
                    bubbles.Stop();
            }
        }
        
        /// <summary>
        /// Turns the front lights on and off.
        /// </summary>
        /// <param name="isEnable">Are the lights on?</param>
        public void LightsEnable(bool isEnable)
        {
            if (lights != null)
            {
                lights.enabled = isEnable;
                ShipManager.Instance.lightsOn = isEnable;
            }
        }

        /// <summary>
        /// A secondary action that depends of the context.
        /// </summary>
        public delegate void SecondaryAction();
        public SecondaryAction onSecondaryAction;
    }
}
