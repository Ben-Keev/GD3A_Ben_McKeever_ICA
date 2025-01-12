using GD.Audio;
using GD.Events;
using GD.Types;
using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GD.Items
{
    /// <summary>
    /// Manages the players inventory, listens for events, etc.
    /// </summary>
    /// <see cref="Inventory"/>
    /// <see cref="ItemData"/>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        [InlineEditor]
        [Tooltip("The player's inventory collection (e.g. a saddlebag")]
        private InventoryCollection inventoryCollection;

        [SerializeField]
        [InlineEditor]
        [Tooltip("The event to raise on succesful/unsuccesful inventory insertion")]
        private IntGameEvent onScoreEvent;

        [SerializeField]
        [InlineEditor]
        [Tooltip("Trigger particles on an inventory insertion")]
        private ParticleGameEvent onParticleEvent;

        [FoldoutGroup("SelectedInventory")]
        [SerializeField]
        [Tooltip("Currently selected inventory")]
        private int selectedInventory;

        [FoldoutGroup("SelectedInventory")]
        [SerializeField]
        [Tooltip("All possible items")]
        private ItemData[] possibleItems = new ItemData[3];

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("Event to end tutorial")]
        private BoolGameEvent onTutorialComplete;

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("Send feedback to helper signs")]
        private FeedbackGameEvent onFeedbackCreated;

        [FoldoutGroup("Sound", expanded: true)]
        [SerializeField]
        [Tooltip("The audio clip that represents absence of an item")]
        private AudioClip noItemClip;

        [FoldoutGroup("Sound", expanded: true)]
        [SerializeField]
        [Tooltip("Plays when correct choice made")]
        private AudioClip correctClip;

        [FoldoutGroup("Sound", expanded: true)]
        [SerializeField]
        [Tooltip("Plays when incorrect choice made")]
        private AudioClip incorrectClip;

        private void Awake()
        {
            //check if the inventory collection has been added
            if (inventoryCollection == null)
                throw new NullReferenceException("No inventory collection has been added");

            selectedInventory = 0;
            UpdateUI();

            inventoryCollection.ClearInventories();
        }

        public void CycleInventoryUp(InputAction.CallbackContext context)
        {
            selectedInventory++;

            if (selectedInventory >= 3)
                selectedInventory = 0;

            UpdateUI();
        }

        public void CycleInventoryDown(InputAction.CallbackContext context)
        {
            selectedInventory--;

            if (selectedInventory <= -1)
                selectedInventory = 2;
        
            UpdateUI();
        }

        /// <summary>
        /// Called on InventoryChange or CycleInventory
        /// </summary>
        public void UpdateUI()
        {
            ItemCategoryType target = (ItemCategoryType)selectedInventory;

            UIManager.Instance.SelectedInventory.sprite = inventoryCollection.Get(target).uiIcon;
            UIManager.Instance.ItemsLeft.text = inventoryCollection.Get(target).Count(possibleItems[(int) target]).ToString();
        }

        private void UpdateSelectedInventoryUI()
        {
            ItemCategoryType target = (ItemCategoryType)selectedInventory;
        }

        /// <summary>
        /// Adds the item to the inventory, taking InventoryCollection as a parameter
        /// </summary>
        /// <param name="data"></param>
        public void OnBinDeposit(Tuple<Transform, BinData> bin)
        {
            FeedbackType feedback;
            ItemData selectedItem = possibleItems[selectedInventory];

            if (inventoryCollection.Get((ItemCategoryType) selectedInventory).isEmpty())
            {
                feedback = FeedbackType.NoItem;

                AudioManager.Instance.PlaySound(noItemClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(bin.Item1, feedback);
            }
            else if (selectedInventory != (int)bin.Item2.BinType)
            {
                feedback = FeedbackType.Wrong;

                DepositIntoBinInventory(bin.Item2, selectedItem, -1);

                AudioManager.Instance.PlaySound(incorrectClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(bin.Item1, feedback);

                SendFeedBack(bin.Item2.BinType, selectedItem, feedback);
            }
            else
            {
                feedback = FeedbackType.Correct;

                // Raise the event only once
                if(onTutorialComplete != null)
                {
                    onTutorialComplete.Raise(true);
                    onTutorialComplete = null;
                }

                DepositIntoBinInventory(bin.Item2, selectedItem, selectedItem.Value);

                AudioManager.Instance.PlaySound(correctClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(bin.Item1, feedback);

                SendFeedBack(bin.Item2.BinType, selectedItem, feedback);
            }
        }

        private void DepositIntoBinInventory(BinData bin, ItemData item, int score)
        {
            onScoreEvent?.Raise(score);
            inventoryCollection.Get((ItemCategoryType)selectedInventory).Remove(item, 1);
            bin.BinContents.Add(item, 1);
        }

        private void SendFeedBack(ItemCategoryType expected, ItemData actual, FeedbackType feedback)
        {
            Tuple<ItemCategoryType, ItemData, FeedbackType> feedbackData = new Tuple<ItemCategoryType, ItemData, FeedbackType>(expected, actual, feedback);
            onFeedbackCreated?.Raise(feedbackData);
        }

        private void SendParticleFeedback(Transform transform, FeedbackType feedback)
        {
            Tuple<Transform, Enum> particleData = new Tuple<Transform, Enum>(transform, feedback);
            onParticleEvent?.Raise(particleData);
        }

        /// <summary>
        /// Adds the item to the inventory.
        /// </summary>
        /// <param name="data"></param>
        public void OnInventoryAdd(ItemData data)
        {
            inventoryCollection.Add(data);
        }
    }
}