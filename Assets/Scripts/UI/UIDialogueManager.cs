using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;
using TMPro;
using Underwater.Submarines;

namespace Underwater.UI
{
    /// <summary>
    /// Handles the dialogues with NPCs.
    /// </summary>
    public class UIDialogueManager : MonoBehaviour
    {
        #region Singleton

        private static UIDialogueManager _instance;
        public static UIDialogueManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("UIDialogueManager is NULL");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion

        /// <summary>
        /// Contains the NPC's message.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _textNPC;
        /// <summary>
        /// Contains the Players' possible answers.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI[] _textChoices;
        /// <summary>
        /// Reference to the NPC communication panel.
        /// </summary>
        [SerializeField]
        private GameObject _npcComPanel;
        /// <summary>
        /// Avatar for the NPC talking.
        /// </summary>
        [SerializeField]
        private Image _npcAvatar;
        /// <summary>
        /// Name of the NPC talking.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _npcName;

        /// <summary>
        /// Called when the communication with a NPC begins.
        /// </summary>
        /// <param name="dialog">Dialogue lines for this NPC.</param>
        /// <param name="npcAvatar">Avatar of the NPC.</param>
        /// <param name="npcName">Name of the NPC.</param>
        public void OpenNPCCom(VIDE_Assign dialog, Sprite npcAvatar, string npcName)
        {
            if (!VD.isActive)
            {
                _npcComPanel.SetActive(true);

                ShipManager.Instance.StopShip();
                Cursor.visible = true;

                _npcAvatar.sprite = npcAvatar;
                _npcName.text = npcName;

                VD.OnNodeChange += UpdateUI;
                VD.OnEnd += End;
                VD.BeginDialogue(dialog);
            }
        }

        /// <summary>
        /// Updates the dialogue panel with new data.
        /// </summary>
        /// <param name="data">Data containing the lines of this dialogue.</param>
        private void UpdateUI(VD.NodeData data)
        {
            if (data.isPlayer)
            {
                for (int i = 0; i < _textChoices.Length; i++)
                {
                    if (i < data.comments.Length)
                    {
                        _textChoices[i].transform.parent.gameObject.SetActive(true);
                        _textChoices[i].text = data.comments[i];
                    }
                    else
                    {
                        _textChoices[i].transform.parent.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                _textNPC.text = data.comments[data.commentIndex];
                VD.Next();
            }
        }

        /// <summary>
        /// Tells what choice of answer the player made.
        /// </summary>
        /// <param name="choice">The answer chosen by the player.</param>
        public void SetPlayerChoice(int choice)
        {
            VD.nodeData.commentIndex = choice;
            VD.Next();
        }

        /// <summary>
        /// Called when the dialogue reached its end.
        /// </summary>
        /// <param name="data">Data containing the lines of this dialogue.</param>
        private void End(VD.NodeData data)
        {
            _npcComPanel.SetActive(false);
            ShipManager.Instance.UnstopShip();
            Cursor.visible = false;

            VD.OnNodeChange -= UpdateUI;
            VD.OnEnd -= End;
            VD.EndDialogue();
        }
    }
}
