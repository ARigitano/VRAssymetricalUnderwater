using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Underwater.UI
{
    /// <summary>
    /// Displays the most importants values of the selected ship to the non VR player.
    /// </summary>
    public class UIOrbit : MonoBehaviour
    {
        #region Singleton

        private static UIOrbit _instance;
        public static UIOrbit Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("UIOrbit is NULL");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion

        /// <summary>
        /// Displays the current health of the selected ship.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _healthText;
        /// <summary>
        /// Displays the max health of the selected ship.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _healthMaxText;
        /// <summary>
        /// Displays the current number of loaded torpedoes of the selected ship.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _torpedoText;
        /// <summary>
        /// Displays the max number of loaded torpedoes of the selected ship.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _torpedoMaxText;

        private void Start()
        {
            Cursor.visible = false;
        }

        /// <summary>
        /// Updates the current health number.
        /// </summary>
        /// <param name="value">The value to display.</param>
        public void UpdateHealthText(int value)
        {
            _healthText.text = value.ToString();
            Debug.Log("health updated");
        }

        /// <summary>
        /// Updates the max health number.
        /// </summary>
        /// <param name="value">The value to display.</param>
        public void UpdateHealthMaxText(int value)
        {
            _healthMaxText.text = "/" + value.ToString();
        }

        /// <summary>
        /// Updates the current torpedo number.
        /// </summary>
        /// <param name="value">The value to display.</param>
        public void UpdateTorpedoText(int value)
        {
            _torpedoText.text = value.ToString();
        }

        /// <summary>
        /// Updates the max health number.
        /// </summary>
        /// <param name="value">The value to display.</param>
        public void UpdatetorpedoMaxText(int value)
        {
            _torpedoMaxText.text = "/" + value.ToString();
        }
    }
}
