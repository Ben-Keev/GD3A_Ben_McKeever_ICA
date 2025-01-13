using GD.Audio;
using GD.Events;
using GD.Types;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        /// <summary>
        /// Change selected inventory and update the UI to indicate so
        /// </summary>
        /// <param name="context">Scrollwheel</param>
        public void CycleInventoryUp(InputAction.CallbackContext context)
        {
            selectedInventory++;

            if (selectedInventory >= 3)
                selectedInventory = 0;

            UpdateUI();
        }

        /// <summary>
        /// Change selected inventory and update the UI to indicate so
        /// </summary>
        /// <param name="context">Scrollwheel</param>
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

        /// <summary>
        /// Deposits from player inventory into bin inventory giving visual and aural feedback
        /// as to whether the choice was currect
        /// </summary>
        /// <param name="bin">Tuple containing transform and bin data of the bin object interacted with</param>
        public void OnBinDeposit(Tuple<Transform, BinData> bin)
        {
            FeedbackType feedback;
            ItemData currentlySelectedItem = possibleItems[selectedInventory];

            // Make code more readable
            Transform binTransform = bin.Item1;
            BinData binData = bin.Item2;

            // The player did not have items in their selected inventory to put into the bin
            if (inventoryCollection.Get((ItemCategoryType) selectedInventory).isEmpty())
            {
                feedback = FeedbackType.NoItem;

                AudioManager.Instance.PlaySound(noItemClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(binTransform, feedback);
            }

            // The player put the wrong item into the bin
            else if (selectedInventory != (int)binData.BinType)
            {
                feedback = FeedbackType.Wrong;

                DepositIntoBinInventory(binData, currentlySelectedItem, -1);

                AudioManager.Instance.PlaySound(incorrectClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(binTransform, feedback);

                SendFeedBack(binData.BinType, currentlySelectedItem, feedback);
            }

            // The player put the correct item in the correct bin
            else
            {
                feedback = FeedbackType.Correct;

                // This action is mandatory for the tutorial
                // Raise the event only once
                if(onTutorialComplete != null)
                {
                    onTutorialComplete.Raise(true);
                    onTutorialComplete = null;
                }

                // If there's three of the given item allow the player to deposit 3 at once.
                if (inventoryCollection[(ItemCategoryType) selectedInventory].Count(currentlySelectedItem) >=3)
                    DepositIntoBinInventory(binData, currentlySelectedItem, currentlySelectedItem.Value, 3);
                else
                    DepositIntoBinInventory(binData, currentlySelectedItem, currentlySelectedItem.Value, 1);

                AudioManager.Instance.PlaySound(correctClip, AudioMixerGroupName.SFX);

                SendParticleFeedback(binTransform, feedback);

                SendFeedBack(binData.BinType, currentlySelectedItem, feedback);
            }
        }

        /// <summary>
        /// Take an item from the player's inventory into the bins inventory
        /// </summary>
        /// <param name="bin"></param>
        /// <param name="item"></param>
        /// <param name="score">Award/Penalty for putting this item in this bin</param>
        /// <param name="itemQuantity">How much of the item is deposited at once</param>
        private void DepositIntoBinInventory(BinData bin, ItemData item, int score, int itemQuantity = 1)
        {
            onScoreEvent?.Raise(score * itemQuantity);
            inventoryCollection.Get((ItemCategoryType)selectedInventory).Remove(item, itemQuantity);
            bin.BinContents.Add(item, itemQuantity);
        }

        /// <summary>
        /// Pack feedback variables into a tuple to raise a feedback event
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="feedback"></param>
        private void SendFeedBack(ItemCategoryType expected, ItemData actual, FeedbackType feedback)
        {
            Tuple<ItemCategoryType, ItemData, FeedbackType> feedbackData = new Tuple<ItemCategoryType, ItemData, FeedbackType>(expected, actual, feedback);
            onFeedbackCreated?.Raise(feedbackData);
        }

        /// <summary>
        /// Pack particle variables into a tuple to raise a particle event
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="feedback"></param>
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